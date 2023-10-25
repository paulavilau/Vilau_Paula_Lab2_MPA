using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
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
                      new Book { Title = "Baltagul", Price = Decimal.Parse("22") },
                      new Book { Title = "Enigma Otiliei", Price = Decimal.Parse("18") },
                      new Book { Title = "Maytrei", Price = Decimal.Parse("27") },
                      new Book { Title = "De veghe in lanul de secara", Price = Decimal.Parse("27") },
                      new Book { Title = "Panza de paianjen", Price = Decimal.Parse("27") },
                      new Book { Title = "Fata de hartie", Price = Decimal.Parse("27") }
                  );

                  context.Customers.AddRange(
                      new Customer { Name = "Popescu Marcela", Adress = "Str. Plopilor, nr. 24", BirthDate = DateTime.Parse("1979-09-01") },
                      new Customer { Name = "Mihailescu Cornel", Adress = "Str. Bucuresti, nr. 45, ap. 2", BirthDate = DateTime.Parse("1969-07-08") }
                  );

                  context.Authors.AddRange(
                      new Author { FirstName = "Mihail", LastName = "Sadoveanu" },
                      new Author { FirstName = "Camil", LastName = "Petrescu" },
                      new Author { FirstName = "J.D.", LastName = "Salinger" },
                      new Author { FirstName = "Guillaume", LastName = " Musso" },
                      new Author { FirstName = "Cella", LastName = "Serghi" },
                      new Author { FirstName = "Mircea", LastName = "Eliade" }
                      );

                  context.SaveChanges();

                  var orders = new Order[]
                   {
                       new Order{BookID=9, CustomerID=1, OrderDate=DateTime.Parse("2021-02-25")},
                       new Order{BookID=10, CustomerID=2, OrderDate=DateTime.Parse("2021-09-28")},
                       new Order{BookID=11, CustomerID=1, OrderDate=DateTime.Parse("2021-10-28")},
                       new Order{BookID=12, CustomerID=2, OrderDate=DateTime.Parse("2021-09-28")},
                       new Order{BookID=20, CustomerID=1, OrderDate=DateTime.Parse("2021-09-28")},
                       new Order{BookID=21, CustomerID=2, OrderDate=DateTime.Parse("2021-10-28")},
                   };

                  foreach (Order e in orders)
                  {
                      context.Orders.Add(e);
                  }
                 

                var publishers = new Models.Publisher[]
                {
                    new Models.Publisher{PublisherName="Humanitas", Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
                    new Models.Publisher{PublisherName="Nemira", Adress="Str. Plopilor, nr. 35,Ploiesti"},
                    new Models.Publisher{PublisherName="Paralela 45", Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
                };

                foreach (Models.Publisher p in publishers)
                {
                    context.Publishers.Add(p);
                }

                context.SaveChanges();
                  
                
                var books = context.Books;
                var publishedbooks = new PublishedBook[]
                {
                    new PublishedBook {
                        BookID = books.Single(c => c.Title == "Maytrei" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName =="Humanitas").ID
                    },
                    new PublishedBook {
                        BookID = books.Single(c => c.Title == "Enigma Otiliei" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Humanitas").ID
                    },
                    new PublishedBook {
                        BookID = books.Single(c => c.Title == "Baltagul" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName =="Nemira").ID
                    },
                    new PublishedBook {
                        BookID = books.Single(c => c.Title == "Fata de hartie" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Paralela45").ID
                    },
                    new PublishedBook {
                        BookID = books.Single(c => c.Title == "Panza de paianjen" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID
                    },
                    new PublishedBook {
                        BookID = books.Single(c => c.Title == "De veghe in lanul de secara" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID
                    },
                };

                foreach (PublishedBook pb in publishedbooks)
                {
                    context.PublishedBooks.Add(pb);
                }

                context.SaveChanges();
            }
            }
        }
}
