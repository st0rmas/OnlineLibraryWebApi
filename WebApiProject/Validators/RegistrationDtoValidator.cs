using FluentValidation;
using WebApiProject.Configuration;
using WebApiProject.Dtos.Auth;

namespace WebApiProject.Validators;

/// <summary>
/// Валидатор для RegistrationDto
/// </summary>
public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
{
	private readonly ApplicationContext _db;

	/// <summary>
	/// Конструктор для валидатора RegistrationDto
	/// </summary>
	/// <param name="db">Контекст базы данных</param>
	public RegistrationDtoValidator(ApplicationContext db)
	{
		_db = db;
		RuleFor(registerDto => registerDto.Login).NotNull().Length(4, 30)
			.WithMessage("Логин должен быть длиной от 3 до 30 символов");
		RuleFor(registerDto => registerDto.Password).NotNull().Length(4, 30)
			.WithMessage("Пароль должен быть длиной от 3 до 30 символов");
		RuleFor(registerDto => registerDto.Login).Must(IsLoginUnique)
			.WithMessage("Пользователь с таким логином уже существует");
	}

	private bool IsLoginUnique(string login)
	{
		var logins = _db.Users.Select(user => user.Login).ToList();
		if (logins.Contains(login))
		{
			return false;
		}

		return true;
	}
}