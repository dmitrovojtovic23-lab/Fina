namespace ConsoleApp38.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}