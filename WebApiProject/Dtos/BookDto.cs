using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Dtos;
/// <summary>
/// DTO модель Book
/// </summary>
public record class BookDto
{
	/// <summary>
	/// Название книги
	/// </summary>
	public required string Name { get; init; }
	/// <summary>
	/// Год публикации книги
	/// </summary>
	public required int PublicationYear { get; init; }
	/// <summary>
	/// Уникальный идентификатор автора
	/// </summary>
	public required Guid AuthorId { get; init; }
}