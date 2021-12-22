using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASP_Meeting_20.Models
{
    public class BookShopContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public BookShopContext(DbContextOptions<BookShopContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Firstname = "Клиффорд", Surname = "Саймак", DateOfBirth = new DateTime(1904, 8, 03) },
                new Author { Id = 2, Firstname = "Джордж", Surname = "Оруэлл", DateOfBirth = new DateTime(1903, 6, 25) }
                );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Пересадочная станция", Year = 1963, Price = 299.9, AuthorId = 1 },
                new Book { Id = 2, Title = "1984", Year = 1949, Price = 199.9, AuthorId = 2 },
                new Book { Id = 3, Title = "Скотный двор", Year = 1945, Price = 249.9, AuthorId = 2 }
                );
        }
    }
}
