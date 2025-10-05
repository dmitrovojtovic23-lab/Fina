﻿namespace ConsoleApp38.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}