using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public static class DtoFactory
    {

        public static Item ItemAddReuestToItem(ItemModelForUI itemAddRequest, Guid locationID)
        {
            var itemToAdd = new Item();
            itemToAdd.ItemId = Guid.NewGuid();
            itemToAdd.ItemName = itemAddRequest.ItemName;
            itemToAdd.PurchaseId = itemAddRequest.PurchaseId;
            itemToAdd.ItemLocationId = locationID;
            itemToAdd.LocationName = itemAddRequest.LocationName;
            itemToAdd.Description = itemAddRequest.Description;
            itemToAdd.CreateUserId = itemAddRequest.CreateUserId;
            itemToAdd.Status = itemAddRequest.Status;
            itemToAdd.CreateUser = itemAddRequest.CreateUser;
            

            return itemToAdd;
        }

        public static ItemModelForUI ItemToItemForUI(Item item)
        {
            var itemResponse = new ItemModelForUI
            {
               
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                LocationName = item.LocationName,
                Description = item.Description,
                Status = item.Status,

            };
            return itemResponse;
        }
        public static ItemLocationResponse LocationToLocationForUI(ItemLocation itemLocationResponse)
        {
            var locationResponse = new ItemLocationResponse
            {
                LocationId = itemLocationResponse.LocationId,
                LocationName = itemLocationResponse.LocationName,
                LocationDetail = itemLocationResponse.LocationDetail,
            };
            return locationResponse;
        }

        public static ItemLocation ItemLocationAddRequest(ItemLocation itemLocationAddRequest)
        {
            ItemLocation itemLocation = new ItemLocation
            {
               
                LocationId = Guid.NewGuid(),
                LocationName = itemLocationAddRequest.LocationName,
                LocationDetail = itemLocationAddRequest.LocationDetail
            };
            return itemLocation;
        }
    }
}
