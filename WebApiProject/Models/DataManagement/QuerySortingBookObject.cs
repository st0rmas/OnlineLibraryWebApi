namespace WebApiProject.Models.DataManagement;

/// <summary>
/// Объект командной строки, отвечающий за сортировку. 
/// </summary>
/// <remarks>
/// Хранит параметры, которые определяют, как будет сортироваться коллекция
/// </remarks>
public record class QuerySortingBookObject
{
	/// <summary>
	/// Хранит название параметра, по которому будет проводиться сортировка. 
	/// </summary>
	/// <remarks>Значение по умолчанию = "book" </remarks>
	public string SortBy { get; init; } = "book";
	/// <summary>
	/// Хранит булевое значение, которое определяет направление соритровки (по убыванию или по возрастанию)
	/// </summary>
	/// <remarks>Значение по умолчанию = false </remarks>
	public bool IsDescending { get; init; } = false;
}