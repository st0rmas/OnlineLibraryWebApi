using Microsoft.EntityFrameworkCore;
using WebApiProject.Configuration;
using WebApiProject.Dtos.Auth;
using WebApiProject.Models;

namespace WebApiProject.Services;

public class UserService : IUserService
{
	private readonly ApplicationContext _db;

	public UserService(ApplicationContext db)
	{
		_db = db;
	}

	public async Task<User?> GetUserByLogin(string login)
	{
		var user = await _db.Users.FirstOrDefaultAsync(user => user.Login == login);
		if (user is not null)
		{
			return user;
		}
		return null;
	}

	public async Task<User?> RegisterUser(RegistrationDto registrationDto)
	{
		var user = new User
		{
			Login = registrationDto.Login,
			Password = registrationDto.Password,
			Role = Role.USER,
		};
		await _db.Users.AddAsync(user);
		await _db.SaveChangesAsync();
		return user;
	}
}