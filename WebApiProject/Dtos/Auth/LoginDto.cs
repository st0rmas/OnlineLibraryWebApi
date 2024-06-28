namespace WebApiProject.Dtos.Auth;

/// <summary>
/// DTO модель Login
/// </summary>
public record class LoginDto
{
	/// <summary>
	/// Логин пользователя
	/// </summary>
	public required string Login { get; init; }
	/// <summary>
	/// Пароль пользователя
	/// </summary>
	public required string Password { get; init; }
}