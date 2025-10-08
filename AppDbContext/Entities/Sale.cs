namespace ConsoleApp38.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int? StoreId { get; set; }
        public Store? Store { get; set; }
        public Payment? Payment { get; set; }
    }
}