using Microsoft.EntityFrameworkCore;
using WebApiProject.Configuration;
using WebApiProject.Dtos;
using WebApiProject.Models;
using WebApiProject.Models.DataManagement;

namespace WebApiProject.Services;

public class BookService : IBookService
{
	private readonly ApplicationContext _db;
	private readonly IAuthorService _authorService;

	public BookService(ApplicationContext db, IAuthorService authorService)
	{
		_db = db;
		_authorService = authorService;
	}

	public async Task<List<Book>> GetAllBooks(QueryFilteringBookObject? queryFilteringBookObject,
		QuerySortingBookObject? querySortingBookObject,
		QueryPaginationBookObject? queryPaginationBookObject)
	{
		var booksQuery = _db.Books.Include(book => book.Author).AsQueryable();
		if (queryPaginationBookObject is not null)
		{
			Pagination(ref booksQuery, queryPaginationBookObject);
		}

		if (queryFilteringBookObject is not null)
		{
			Filtering(ref booksQuery, queryFilteringBookObject);
		}

		if (querySortingBookObject is not null)
		{
			Sorting(ref booksQuery, querySortingBookObject);
		}

		var books = await booksQuery.ToListAsync();
		return books;
	}

	public async Task<Book> GetBookById(Guid id)
	{
		var book = await _db.Books.Include(book => book.Author).Include(book=>book.Users).FirstOrDefaultAsync(book => book.Id == id);
		if (book is not null)
		{
			return book;
		}

		return null;
	}

	public async Task<bool> CreateBook(BookDto bookDto)
	{
		var author = await _authorService.GetAuthorById(bookDto.AuthorId);

		if (author is not null)
		{
			Book book = new Book
			{
				Id = new Guid(),
				Name = bookDto.Name,
				PublicationYear = bookDto.PublicationYear,
				AuthorId = author.Id,
				Author = author
			};
			_db.Books.Add(book);
			await _db.SaveChangesAsync();
			return true;
		}

		return false;
	}

	public async Task<bool> EditBook(Guid bookIdToUpdate, BookDto newBookDto)
	{
		var bookToUpdate = GetBookById(bookIdToUpdate).Result;
		if (bookToUpdate is not null)
		{
			bookToUpdate.Name = newBookDto.Name;
			bookToUpdate.PublicationYear = newBookDto.PublicationYear;
			bookToUpdate.AuthorId = newBookDto.AuthorId;
			_db.Update(bookToUpdate);
			await _db.SaveChangesAsync();
			return true;
		}

		return false;
	}

	public async Task DeleteBook(Guid id)
	{
		var book = await _db.Books.FirstOrDefaultAsync(book => book.Id == id);
		if (book is not null)
		{
			_db.Books.Remove(book);
			await _db.SaveChangesAsync();
		}
	}

	public async Task<List<Book>> GetBooksByAuthor(Guid id)
	{
		return await _db.Books.Where(book => book.AuthorId == id).ToListAsync();
	}

	private void Pagination(ref IQueryable<Book> booksQuery, QueryPaginationBookObject queryPaginationBookObject)
	{
		if (queryPaginationBookObject.PageSize > 0 && queryPaginationBookObject.Page > 0)
		{
			booksQuery = booksQuery.Skip(queryPaginationBookObject.PageSize * (queryPaginationBookObject.Page - 1))
				.Take(queryPaginationBookObject.PageSize);
		}
	}

	private void Filtering(ref IQueryable<Book> booksQuery, QueryFilteringBookObject queryFilteringBookObject)
	{
		if (!string.IsNullOrEmpty(queryFilteringBookObject.AuthorName))
		{
			booksQuery = booksQuery.Where(book => book.Author.Name == queryFilteringBookObject.AuthorName);
		}

		if (queryFilteringBookObject.MinPublicationYear <= queryFilteringBookObject.MaxPublicationYear)
		{
			booksQuery = booksQuery.Where(book => book.PublicationYear >= queryFilteringBookObject.MinPublicationYear)
				.Where(book => book.PublicationYear <= queryFilteringBookObject.MaxPublicationYear);
		}
	}

	private void Sorting(ref IQueryable<Book> booksQuery, QuerySortingBookObject querySortingBookObject)
	{
		string sortBy = querySortingBookObject.SortBy.ToLower();
		bool isDescending = querySortingBookObject.IsDescending;
		switch (sortBy)
		{
			case "book":
				booksQuery = isDescending
					? booksQuery.OrderByDescending(book => book.Name)
					: booksQuery = booksQuery.OrderBy(book => book.Name);
				break;
			case "author":
				booksQuery = isDescending
					? booksQuery.OrderByDescending(book => book.Author.Name)
					: booksQuery = booksQuery.OrderBy(book => book.Author.Name);
				break;
			case "year":
				booksQuery = isDescending
					? booksQuery.OrderByDescending(book => book.PublicationYear)
					: booksQuery = booksQuery.OrderBy(book => book.PublicationYear);
				break;
			default:
				booksQuery = isDescending
					? booksQuery.OrderByDescending(book => book.Name)
					: booksQuery = booksQuery.OrderBy(book => book.Name);
				break;
		}
	}
}