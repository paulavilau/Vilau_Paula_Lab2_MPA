﻿using Microsoft.EntityFrameworkCore;
using Vilau_Paula_Lab2.Models;

namespace Vilau_Paula_Lab2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context.Books.Any())
                {
                    return; // BD a fost creata anterior
                }
                context.Books.AddRange(
                    new Book { Title = "Baltagul", Price = Decimal.Parse("22")}, 
                    new Book { Title = "Enigma Otiliei", Price = Decimal.Parse("18")}, 
                    new Book { Title = "Maytrei", Price = Decimal.Parse("27")});

                context.Customers.AddRange(
                    new Customer { Name = "Popescu Marcela", Adress = "Str. Plopilor, nr. 24", BirthDate = DateTime.Parse("1979-09-01") }, 
                    new Customer { Name = "Mihailescu Cornel", Adress = "Str. Bucuresti, nr. 45, ap. 2", BirthDate = DateTime.Parse("1969-07-08") }
                );
                context.Authors.AddRange(
                    new Author { FirstName = "Mihail", LastName = "Sadoveanu" },
                    new Author { FirstName = "Camil", LastName = "Petrescu" }
                    );
                context.SaveChanges();
                }
            }
        }
}
