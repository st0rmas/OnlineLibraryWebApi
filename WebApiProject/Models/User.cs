namespace WebApiProject.Models;

/// <summary>
/// Сущность Пользователь
/// </summary>
public record class User
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public int Id { get; init; }
	/// <summary>
	/// Логин пользователя
	/// </summary>
	public required string Login { get; init; }
	/// <summary>
	/// Пароль пользователя
	/// </summary>
	public required string Password { get; init; }
	/// <summary>
	/// Роль пользователя
	/// </summary>
	public required Role Role { get; init; }
	/// <summary>
	/// Список книг, которые пользователь купил
	/// </summary>
	public ISet<Book> Books { get; init; } = new HashSet<Book>();
}