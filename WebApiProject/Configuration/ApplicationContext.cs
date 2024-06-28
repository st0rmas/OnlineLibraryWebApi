using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Configuration;

public class ApplicationContext : DbContext
{
	public DbSet<Book> Books { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<User> Users { get; set; }

	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Названия таблиц
		modelBuilder.Entity<Book>().ToTable("books");
		modelBuilder.Entity<Author>().ToTable("authors");
		
		// Отношения таблиц
		modelBuilder.Entity<Author>()
			.HasMany(author => author.Books)
			.WithOne(book => book.Author)
			.HasForeignKey(book => book.AuthorId);
		modelBuilder.Entity<Book>()
			.HasMany<User>(book => book.Users)
			.WithMany(user => user.Books)
			.UsingEntity(entity=>entity.ToTable("users_books"));

		// Ограничение уникальный столбец
		modelBuilder.Entity<Book>().HasIndex(book => book.Name).IsUnique();
		modelBuilder.Entity<Author>().HasIndex(author => author.Name).IsUnique();
		modelBuilder.Entity<User>().HasIndex(user => user.Login).IsUnique();
	}
}