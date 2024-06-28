using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiProject.Consts;
using WebApiProject.Dtos.Auth;
using WebApiProject.Services;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace WebApiProject.Controllers;

/// <summary>
/// Контроллер для работы с авторизацией пользователя
/// </summary>
[ApiController]
[Route("/api/v1/")]
public class AuthController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly IValidator<LoginDto> _loginValidator;
	private readonly IValidator<RegistrationDto> _registrationValidator;

	/// <summary>
	/// Конструктор для AuthController
	/// </summary>
	/// <param name="userService">Реализует интерфейс IUserService</param>
	/// <param name="loginValidator">Валидатор для LoginDto</param>
	/// <param name="registrationValidator">Валидатор для RegisterDto</param>
	public AuthController(IUserService userService, IValidator<LoginDto> loginValidator,
		IValidator<RegistrationDto> registrationValidator)
	{
		_userService = userService;
		_loginValidator = loginValidator;
		_registrationValidator = registrationValidator;
	}

	/// <summary>
	/// Метод для авторизации пользователя
	/// </summary>
	/// <param name="loginDto">Модель LoginDto</param>
	/// <returns>JWT Токен для пользователя</returns>
	/// <exception cref="ValidationException">Возникает в случае ошибки валидации LoginDto</exception>
	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginDto loginDto)
	{
		var validationResult = await _loginValidator.ValidateAsync(loginDto);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		var user = await _userService.GetUserByLogin(loginDto.Login);
		if (user is null)
		{
			return BadRequest($"Пользователь с логином {loginDto.Login} не найден");
		}

		if (user.Password != loginDto.Password)
		{
			return BadRequest("Неправильный пароль");
		}

		var claims = new List<Claim>()
		{
			new Claim("login", loginDto.Login),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};
		var jwt = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
			signingCredentials: new SigningCredentials(JwtConsts.GetSymmetricSecurityKey(),
				SecurityAlgorithms.HmacSha256));

		return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
	}

	/// <summary>
	/// Метод для регистрации польхователя
	/// </summary>
	/// <param name="registrationDto">Модель RegisterDto</param>
	/// <returns>Обхект класса User созданного пользователя</returns>
	/// <exception cref="ValidationException">Возникает в случае ошибки валидации RegisterDto</exception>
	[HttpPost("signup")]
	public async Task<IActionResult> Register(RegistrationDto registrationDto)
	{
		var validationResult = await _registrationValidator.ValidateAsync(registrationDto);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		if (registrationDto.Password != registrationDto.RepeatedPassword)
		{
			var validationfailure = new ValidationFailure()
			{
				PropertyName = "RepeatedPassword",
				ErrorMessage = "Пароли не совпадают",
				AttemptedValue = registrationDto.RepeatedPassword,
			};
			throw new ValidationException(new List<ValidationFailure>() { validationfailure });
		}

		var newUser = await _userService.RegisterUser(registrationDto);
		return Ok(newUser);
	}
}