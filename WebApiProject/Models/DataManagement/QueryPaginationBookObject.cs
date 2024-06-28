namespace WebApiProject.Models.DataManagement;

/// <summary>
/// Объект командной строки, отвечающий за пагинацию. 
/// </summary>
/// <remarks>
/// Хранит параметры, которые определяют параметры для пагинации 
/// </remarks>
public record class QueryPaginationBookObject
{
	/// <summary>
	/// Количество лементов на странице
	/// </summary>
	/// <remarks>Значение по умолчанию = 20; </remarks>
	/// <remarks>Значение должно быть больше 0</remarks>
	public int PageSize { get; init; } = 20;
	/// <summary>
	/// Номер страницы
	/// </summary>
	/// <remarks>Значение должно быть больше 0; </remarks>
	/// <remarks>Значение по умолчанию = 1 </remarks>
	public int Page { get; init; } = 1;
}