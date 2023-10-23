using Microsoft.EntityFrameworkCore;
using Vilau_Paula_Lab2.Models;

namespace Vilau_Paula_Lab2.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            modelBuilder.Entity<Customer>().ToTable("Customer"); 
            modelBuilder.Entity<Order>().ToTable("Order"); 
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Author>().ToTable("Author");
        }
    }
}
    
