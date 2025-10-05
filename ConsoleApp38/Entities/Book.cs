namespace ConsoleApp38.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public Reservation Reservations { get; set; }
        public Review Reviews { get; set; }
        public Sale Sales { get; set; }
    }
}