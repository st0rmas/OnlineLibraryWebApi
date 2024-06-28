using Microsoft.EntityFrameworkCore;
using WebApiProject.Configuration;
using WebApiProject.Dtos;
using WebApiProject.Models;

namespace WebApiProject.Services;

public class AuthorService : IAuthorService
{
	private readonly ApplicationContext _db;

	public AuthorService(ApplicationContext db)
	{
		_db = db;
	}	
	public async  Task<List<Author>> GetAuthors()
	{
		return await _db.Authors.ToListAsync();
	}

	public async Task<Author> GetAuthorById(Guid id)
	{
		var author =  await _db.Authors.FirstOrDefaultAsync(author => author.Id == id);
		if (author is not null)
		{
			return author;
		}
		return null;
	}

	public async Task CreateAuthor(AuthorDto authorDto)
	{
		var author = new Author
		{
			Id = new Guid(),
			Name = authorDto.Name
		};
		_db.Authors.Add(author);
		await _db.SaveChangesAsync();
	}
}