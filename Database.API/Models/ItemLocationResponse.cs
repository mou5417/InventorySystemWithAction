namespace Database.API.Models
{
    public class ItemLocationResponse
    {

        public Guid LocationId { get; set; }
        public string LocationDetail { get; set; }
        public string LocationName { get; set; }

        public List<Item> items { get; set; }
    }
}