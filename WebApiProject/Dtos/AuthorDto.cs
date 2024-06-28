namespace WebApiProject.Dtos;

/// <summary>
/// DTO модель Author
/// </summary>
public record class AuthorDto
{
	/// <summary>
	/// Имя автора
	/// </summary>
	public required string Name { get; init; }
}