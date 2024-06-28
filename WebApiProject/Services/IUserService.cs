using WebApiProject.Dtos.Auth;
using WebApiProject.Models;

namespace WebApiProject.Services;

/// <summary>
/// Определяет сервис для сущности User 
/// </summary>
public interface IUserService
{
	/// <summary>
	/// Метод для получения пользователя по логину
	/// </summary>
	/// <param name="login">Логин</param>
	/// <returns>Объект User</returns>
	Task<User?> GetUserByLogin(string login);
	/// <summary>
	/// Метод для регистрации пользователя по входным данным
	/// </summary>
	/// <param name="registrationDto">Объект RegistrationDto</param>
	/// <returns>Объект User</returns>
	Task<User?> RegisterUser(RegistrationDto registrationDto);
}