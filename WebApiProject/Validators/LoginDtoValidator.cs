using FluentValidation;
using WebApiProject.Configuration;
using WebApiProject.Dtos.Auth;

namespace WebApiProject.Validators;

/// <summary>
/// Валидатор для LoginDto
/// </summary>
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
	/// <summary>
	/// Конструктор для валидатора LoginDto
	/// </summary>
	public LoginDtoValidator()
	{
		RuleFor(loginDto => loginDto.Login).NotNull().Length(4, 30)
			.WithMessage("Логин должен быть длиной от 4 до 30 символов");
		RuleFor(loginDto => loginDto.Password).NotNull().Length(4, 30)
			.WithMessage("Пароль должен быть длиной от 4 до 30 символов");
	}

	
}