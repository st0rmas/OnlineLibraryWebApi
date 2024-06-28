using FluentValidation;
using WebApiProject.Dtos;
using WebApiProject.Models;

namespace WebApiProject.Validators;

/// <summary>
/// Валидатор для BookDto
/// </summary>
public class BookDtoValidator : AbstractValidator<BookDto>
{
	/// <summary>
	/// Консруктор для валидатора BookDto
	/// </summary>
	public BookDtoValidator()
	{
		RuleFor(book => book.Name).NotNull().NotEmpty().WithMessage("Название книги не может быть пустым");
		RuleFor(book => book.PublicationYear).ExclusiveBetween(1000, DateTime.Now.Year)
			.WithMessage("Год публикации книги должен быть в диапазоне от 1000 до текущего года");
		RuleFor(book => book.AuthorId).NotNull().WithMessage("Автор должен быть указан");
	}
}