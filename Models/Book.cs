using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vilau_Paula_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public decimal Price { get; set; }
        public Author? Author { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
