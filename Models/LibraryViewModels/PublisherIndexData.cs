namespace Vilau_Paula_Lab2.Models.LibraryViewModels
{
    public class PublisherIndexData
    { 
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
