using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApiProject.Consts;

public static class JwtConsts
{
	/// <summary>
	/// Секретный ключ для авторизации
	/// </summary>
	public static readonly string SecretKey = "super_mega_secret_key_for_jwt_authorization";
	/// <summary>
	///  Метод для лполучения секретного ключа
	/// </summary>
	/// <returns>Symmetric Security Key</returns>
	public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
		new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
}