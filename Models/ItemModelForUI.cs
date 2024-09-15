namespace Entities
{
    public class ItemModelForUI
    {
        public int? Id { get; set; }
        public Guid ItemId { get; set; }

        public string? ItemName { get; set; }

        public string? LocationName { get; set; }

        public string? Description { get; set; }

        public int PurchaseId { get; set; }
        public Guid CreateUserId { get; set; }
        public User CreateUser { get; set; }
        public string Status { get; set; }
    }

}
