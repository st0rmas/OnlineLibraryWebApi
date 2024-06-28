using WebApiProject.Dtos;
using WebApiProject.Models;
using WebApiProject.Models.DataManagement;

namespace WebApiProject.Services;
/// <summary>
/// Определяет сервис для сущности Book
/// </summary>
public interface IBookService
{
	/// <summary>
	/// Получение всех книг.
	/// </summary>
	/// <param name="queryFilteringBookObject">Объект, отвечающий за параметры строки для фильтрации списка Book</param>
	/// <param name="querySortingBookObject">Объект, отвечающий за параметры строки для сортировки списка Book</param>
	/// <param name="queryPaginationBookObject">Объект, отвечающий за параметры строки для пагинации списка Book</param>
	/// <returns></returns>
	public Task<List<Book>> GetAllBooks(QueryFilteringBookObject? queryFilteringBookObject,
		QuerySortingBookObject? querySortingBookObject,
		QueryPaginationBookObject? queryPaginationBookObject);
	/// <summary>
	/// Возвращает книгу по уникальному идентификатору
	/// </summary>
	/// <param name="id">Уникальный идентификатор книги</param>
	/// <returns>Объект Book</returns>
	public Task<Book> GetBookById(Guid id);
	/// <summary>
	/// Создание книги
	/// </summary>
	/// <param name="bookDto">Объект BookDto</param>
	/// <returns>True, если объект создан. Иначе - false</returns>
	public Task<bool> CreateBook(BookDto bookDto);
	/// <summary>
	/// Редактирование книги
	/// </summary>
	/// <param name="bookIdToUpdate">Уникальный идентификатор книги для изменения</param>
	/// <param name="newBookDto">Новый объект BookDto</param>
	/// <returns>True, если объект изменен. Иначе - false</returns>
	public Task<bool> EditBook(Guid bookIdToUpdate, BookDto newBookDto);
	/// <summary>
	/// Удаление книги
	/// </summary>
	/// <param name="id">Уникальный идентификатор книги</param>
	public Task DeleteBook(Guid id);
}