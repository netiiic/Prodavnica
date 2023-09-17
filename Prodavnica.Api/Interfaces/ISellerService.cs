using Prodavnica.Api.Dto;

namespace Prodavnica.Api.Interfaces
{
    public interface ISellerService
    {
        ShoppingItemDto AddNewItemToDB(ShoppingItemDto newItem);
        List<ShoppingItemDto> GetAllMyItems(Guid sellerId);
        ShoppingItemDto UpdateItem(Guid id, ShoppingItemDto shoppingItem);
        bool DeleteItem(Guid id);
        List<OrederDto> GetSellerOrders(Guid sellerID);
    }
}
