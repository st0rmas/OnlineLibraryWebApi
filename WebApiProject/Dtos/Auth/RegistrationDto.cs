namespace WebApiProject.Dtos.Auth;

/// <summary>
/// DTO модель Registration
/// </summary>
public record class RegistrationDto
{
	/// <summary>
	/// Логин пользователя
	/// </summary>
	public required string Login { get; init; }
	/// <summary>
	/// Пароль пользователя
	/// </summary>
	public required string Password { get; init; }
	/// <summary>
	/// Второй пароль для подтверждения. Должен быть равен полю Password
	/// </summary>
	public required string RepeatedPassword { get; init; }
}