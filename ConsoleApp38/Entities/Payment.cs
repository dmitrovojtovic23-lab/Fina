using System.ComponentModel.DataAnnotations;
namespace ConsoleApp38.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string Method { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}