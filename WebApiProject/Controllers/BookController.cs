using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dtos;
using WebApiProject.Models.DataManagement;
using WebApiProject.Services;

namespace WebApiProject.Controllers;

/// <summary>
/// Контроллер для Book
/// </summary>
[Route("api/v1/books")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
	private readonly IBookService _bookService;
	private readonly IAuthorService _authorService;

	private readonly IValidator<BookDto> _bookValidator;

	/// <summary>
	/// Конструктор для BookController
	/// </summary>
	/// <param name="bookService">Класс, реализующий IBookService</param>
	/// <param name="authorService">Класс, реализующий IAuthorService</param>
	/// <param name="bookValidator">Валидатор для BookDto</param>
	public BookController(IBookService bookService, IAuthorService authorService, IValidator<BookDto> bookValidator)
	{
		_bookService = bookService;
		_authorService = authorService;
		_bookValidator = bookValidator;
	}

	/// <summary>
	/// Получение всех книг
	/// </summary>
	/// <param name="queryFilteringBookObject">Объект, отвечающий за параметры строки для фильтрации списка Book</param>
	/// <param name="querySortingBookObject">Объект, отвечающий за параметры строки для сортировки списка Book</param>
	/// <param name="queryPaginationBookObject">Объект, отвечающий за параметры строки для пагинации списка Book</param>
	/// <returns>Список Book</returns>
	[HttpGet("")]
	public async Task<IActionResult> GetBooks([FromQuery] QueryFilteringBookObject? queryFilteringBookObject,
		[FromQuery] QuerySortingBookObject? querySortingBookObject,
		[FromQuery] QueryPaginationBookObject? queryPaginationBookObject)
	{
		var books = await _bookService.GetAllBooks(queryFilteringBookObject, querySortingBookObject, queryPaginationBookObject);
		if (books.Count==0)
		{
			return NotFound("Книги по вашему запросу не найдены");
		}
		return Ok(books);
	}

	/// <summary>
	/// Получение книги по уникальному идентификатору
	/// </summary>
	/// <param name="id">Уникальный идентификатор книги</param>
	/// <returns>Объект Book</returns>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetBookById(Guid id)
	{
		var book = await _bookService.GetBookById(id);
		if (book is null)
		{
			return NotFound("Книга не найдена");
		}
		
		return Ok(book);
	}

	/// <summary>
	/// Создание объекта Book
	/// </summary>
	/// <param name="bookDto">Обхект BookDto</param>
	/// <returns>Строку об успешном создании Book</returns>
	/// <exception cref="ValidationException">Возникает в случае ошикби валидации BookDto</exception>
	[HttpPost("")]
	public async Task<IActionResult> CreateBook(BookDto bookDto)
	{
		ValidationResult result = await _bookValidator.ValidateAsync(bookDto);
		if (!result.IsValid)
		{
			throw new ValidationException(result.Errors);
		}

		bool created = await _bookService.CreateBook(bookDto);
		if (created)
		{
			return Ok("Книга создана");
		}

		return BadRequest("Неверные данные");
	}

	/// <summary>
	/// Изменение Book
	/// </summary>
	/// <param name="id">Уникальный идентификатор Book</param>
	/// <param name="newBookDto">Новый объект BookDto</param>
	/// <returns>Строку об успешном изменении данных Book</returns>
	/// <exception cref="ValidationException">Возникает при ошибки валидации нового объекта BookDto</exception>
	[HttpPut("{id}")]
	public async Task<IActionResult> EditBook(Guid id, BookDto newBookDto)
	{
		if (id == Guid.Empty)
		{
			return BadRequest("Id не может быть пустым");
		}

		var validationResult = await _bookValidator.ValidateAsync(newBookDto);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		bool updated = await _bookService.EditBook(id, newBookDto);
		if (updated)
		{
			return Ok("Данные книги обновлены");
		}

		return BadRequest("Неверные данные");
	}

	/// <summary>
	/// Удаление Book
	/// </summary>
	/// <param name="id">Уникальный идентификатор Boko</param>
	/// <returns>Строку об успешном удалении Book</returns>
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBook(Guid id)
	{
		if (id == Guid.Empty)
		{
			return BadRequest("Неверный Id");
		}

		await _bookService.DeleteBook(id);
		return Ok("Книга удалена");
	}
}