using System.Text.Json.Serialization;

namespace WebApiProject.Models;

/// <summary>
/// Автор книги
/// </summary>
public class Author
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid Id { get; init; } 
	/// <summary>
	/// ФИО автора
	/// </summary>
	public required string Name { get; init; }
	/// <summary>
	/// Список книг, которые написал автор
	/// </summary>
	[JsonIgnore]
	public HashSet<Book> Books { get; init; }
}