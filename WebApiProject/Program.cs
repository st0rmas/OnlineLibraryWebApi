using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using WebApiProject.Configuration;
using WebApiProject.Dtos;
using WebApiProject.Dtos.Auth;
using WebApiProject.Exceptions.ExceptionHandler;
using WebApiProject.Services;
using WebApiProject.Validators;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultDbConnection");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
	};
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(options=>options.UseNpgsql(connectionString));

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
builder.Services.AddScoped<IValidator<AuthorDto>, AuthorDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<RegistrationDto>, RegistrationDtoValidator>();

builder.Services.AddProblemDetails(options =>
{
	options.CustomizeProblemDetails = ctx =>
	{
		ctx.ProblemDetails.Extensions.Add("instance",
			$"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
	};
	
});
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Library API",
		Description = "An ASP.NET Core Web API"
	});
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	options.AddSecurityDefinition
	(
		name: JwtBearerDefaults.AuthenticationScheme,
		securityScheme: new OpenApiSecurityScheme()
		{
			In = ParameterLocation.Header,
			Name = HeaderNames.Authorization,
			Scheme = JwtBearerDefaults.AuthenticationScheme,
			Type = SecuritySchemeType.Http
		}
	);
	options.AddSecurityRequirement
	(
		new OpenApiSecurityRequirement()
		{
			{
				new OpenApiSecurityScheme()
				{
					Reference = new OpenApiReference()
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				},
				Array.Empty<string>()
			}
		});
});

var app = builder.Build();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	});
}

app.Run();