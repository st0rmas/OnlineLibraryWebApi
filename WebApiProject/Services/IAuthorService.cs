using WebApiProject.Dtos;
using WebApiProject.Models;

namespace WebApiProject.Services;

/// <summary>
/// Определяет сервис для сущности Author
/// </summary>
public interface IAuthorService
{
	/// <summary>
	/// Возвращает всех авторов
	/// </summary>
	/// <returns>Список объектов Author</returns>
	public Task<List<Author>> GetAuthors();
	/// <summary>
	/// Возвращает объект Author по уникальному идентификатору
	/// </summary>
	/// <param name="id">Уникальный илдентификатор автора</param>
	/// <returns>Объект Author</returns>
	public Task<Author> GetAuthorById(Guid id);

	/// <summary>
	/// Создание автора
	/// </summary>
	/// <param name="authorDto">Объект AuthorDto</param>
	public Task CreateAuthor(AuthorDto authorDto);
	
}