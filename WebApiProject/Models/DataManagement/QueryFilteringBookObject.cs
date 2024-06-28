namespace WebApiProject.Models.DataManagement;

/// <summary>
/// Объект командной строки, отвечающий за фильтрацию. 
/// </summary>
/// <remarks>
/// Хранит параметры, которые определяют, как будет фильтроваться коллекция
/// </remarks>
public record class QueryFilteringBookObject
{
	/// <summary>
	/// Фильтр для отбора по имени автора
	/// </summary>
	public string? AuthorName { get; init; }
	/// <summary>
	/// Фильтр для минимально допустимого года публикации книги
	/// </summary>
	/// <remarks>Значение по умолчанию = 1000 </remarks>
	public int MinPublicationYear { get; init; } = 1000;
	/// <summary>
	/// Фильтр для максимально допустимого года публикации книги
	/// </summary>
	/// <remarks>Значение по умолчанию = текущий год</remarks>
	public int MaxPublicationYear { get; init; } = DateTime.Now.Year;

}