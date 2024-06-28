using System.Text.RegularExpressions;
using FluentValidation;
using WebApiProject.Dtos;
using WebApiProject.Models;

namespace WebApiProject.Validators;

/// <summary>
/// Валидатор для AuthorDto
/// </summary>
public class AuthorDtoValidator : AbstractValidator<AuthorDto>
{
	/// <summary>
	/// Консруктор для создания валидатора AuthorDto
	/// </summary>
	public AuthorDtoValidator()
	{
		RuleFor(author => author.Name).NotNull().Length(3,40).WithMessage("Имя автора должно быть длиной от 3 до 40 символов");
		RuleFor(author => author.Name).Must(BeRegex).WithMessage("Имя автора должно быть на русском языке и в виде: Иванов И.И.");
	}

	private bool BeRegex(string name)
	{
		Regex regex = new Regex(@"[А-Я([а-я]+\s[А-Я]\.[А-Я]\.");
		MatchCollection matches = regex.Matches(name);
		if (matches.Count>0)
		{
			return true;
		}

		return false;
	}
}