using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Exceptions.ExceptionHandler;

/// <summary>
/// Handler для обработки исключения Validation exception
/// </summary>
public class ValidationExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
		CancellationToken cancellationToken)
	{
		if (exception is ValidationException validationException)
		{
			var problemDetails = new ProblemDetails()
			{
				Status = StatusCodes.Status400BadRequest,
				Title = "Обнаружена одна или несколько ошибок валидации",
				Instance = $"{context.Request.Method} {context.Request.Path}",
				Extensions = new Dictionary<string, object?>()
				{
					{
						"errors", validationException.Errors.Select(error =>
							new { error.PropertyName, error.ErrorMessage, error.AttemptedValue })
					}
				}
			};
			await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
			return true;
		}
		return false;
	}
}