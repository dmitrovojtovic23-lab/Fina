namespace ConsoleApp38.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<City> City { get; set; }
        public int CityId { get; set; }
    }
}