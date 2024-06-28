using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Services;

namespace WebApiProject.Controllers;

/// <summary>
/// Контроллер главной страницы
/// </summary>
[ApiController]
[Route("/api/v1/")]
[Authorize]
public class HomeController : ControllerBase
{
	private readonly IUserService _userService;

	/// <summary>
	/// Конструктор для HomeController
	/// </summary>
	/// <param name="userService"></param>
	public HomeController(IUserService userService)
	{
		_userService = userService;
	}

	/// <summary>
	/// Получение пользователя из контекста. Используется HttpContext.User
	/// </summary>
	/// <returns></returns>
	[HttpGet("profile")]
	public async Task<IActionResult> GetUserProfile()
	{
		string login = User.Claims.FirstOrDefault(claim=>claim.Type == "login")!.Value;
		var user = await _userService.GetUserByLogin(login);
		return Ok(user);
	}
}