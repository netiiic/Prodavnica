using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;

namespace Prodavnica.Api.Services
{
    public class SellerService : ISellerService
    {
        private readonly IRepository _repository;

        public SellerService(IRepository repository)
        {
            _repository = repository;
        }

        public ShoppingItemDto AddNewItemToDB(ShoppingItemDto newItem)
        {
            return _repository.AddNewItemToDB(newItem);
        }

        public bool DeleteItem(Guid id)
        {
            return _repository.DeleteItem(id);
        }

        public List<ShoppingItemDto> GetAllMyItems(Guid sellerId)
        {
            if (!_repository.UserExistsByGuid(sellerId))
            {
                return null;
            }
            return _repository.GetSellerItems(sellerId);
        }

        public List<OrederDto> GetSellerOrders(Guid sellerID)
        {
            if (!_repository.UserExistsByGuid(sellerID))
            {
                return null;
            }
            return _repository.GetSellerOrders(sellerID);
        }

        public ShoppingItemDto UpdateItem(Guid id, ShoppingItemDto shoppingItem)
        {
            ShoppingItemDto changedItem = _repository.UpdateItem(id, shoppingItem);
            return changedItem;
        }
    }
}
