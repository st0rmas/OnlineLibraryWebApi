using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dtos;
using WebApiProject.Services;

namespace WebApiProject.Controllers;

/// <summary>
/// Контроллер для Author
/// </summary>
[ApiController]
[Route("/api/v1/authors")]
[Authorize]
public class AuthorController : ControllerBase
{
	private readonly IAuthorService _authorService;
	private readonly IValidator<AuthorDto> _authorDtoValidator;

	/// <summary>
	/// Конструктор для AuthorController
	/// </summary>
	/// <param name="authorService">Класс, реализующий интерфейс IAuthorService</param>
	/// <param name="authorDtoValidator">Валидатор для AuthorDto</param>
	public AuthorController(IAuthorService authorService, IValidator<AuthorDto> authorDtoValidator)
	{
		_authorService = authorService;
		_authorDtoValidator = authorDtoValidator;
	}

	/// <summary>
	/// Получение всех авторов
	/// </summary>
	/// <returns>Список авторов</returns>
	[HttpGet("")]
	public async Task<IActionResult> GetAllAuthors()
	{
		var authors = await _authorService.GetAuthors();
		return Ok(authors);
	}

	/// <summary>
	/// Получение автора по id
	/// </summary>
	/// <param name="id">Уникальный id автора</param>
	/// <returns>Объект класса Author</returns>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetAuthorById(Guid id)
	{
		var author = await _authorService.GetAuthorById(id);
		if (author is not null)
		{
			return Ok(author);
		}

		return BadRequest("Автор с таким id не найден");
	}

	/// <summary>
	/// Создание объекта Author
	/// </summary>
	/// <param name="authorDto">Объект AuthorDto</param>
	/// <returns>Строку об успешном создании Author</returns>
	/// <exception cref="ValidationException">Возникает в случае ошибки валидации AuthorDto</exception>
	[HttpPost("")]
	public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
	{
		var validationResult = await _authorDtoValidator.ValidateAsync(authorDto);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		await _authorService.CreateAuthor(authorDto);
		return Ok("Автор создан");
	}
}