using System.Text.Json.Serialization;

namespace WebApiProject.Models;
/// <summary>
/// Сущность книга
/// </summary>
public record class Book
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid Id { get; init; }
	/// <summary>
	/// Назване книги
	/// </summary>
	public required string Name { get; set; }
	/// <summary>
	/// Год публикации книги
	/// </summary>
	public required int PublicationYear { get; set; }
	/// <summary>
	/// Уникальный идентификатор автора книги
	/// </summary>
	public required Guid AuthorId { get; set; }
	/// <summary>
	/// Автор книги
	/// </summary>
	public required Author Author { get; init; }
	/// <summary>
	/// Спсок пользователей, купивших книгу 
	/// </summary>
	[JsonIgnore]
	public ISet<User> Users { get; init; } = new HashSet<User>();
}