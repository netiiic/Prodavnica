using Microsoft.AspNetCore.Mvc;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;

namespace Prodavnica.Api.Controllers
{
    [Route("api/seller")]
    public class SellerController : ControllerBase
    {
        private readonly IRepository _repository;

        public SellerController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("AddNewItem")]
        public IActionResult AddNewItem([FromBody] ShoppingItemDto shoppingItemDto)
        {
            return Ok(_repository.AddNewItemToDB(shoppingItemDto));
        }

        [HttpPut]
        [Route("GetAllMyItems")]
        public IActionResult GetAllMyItems(Guid sellerId)
        {
            if(!_repository.UserExistsByGuid(sellerId))
            {
                return BadRequest("You don't exist");
            }
            return Ok(_repository.GetSellerItems(sellerId));
        }

        [HttpPut]
        [Route("UpdateItem")]
        public IActionResult UpdateItem(Guid id, [FromBody] ShoppingItemDto shoppingItemDto)
        {
            ShoppingItemDto changedItem = _repository.UpdateItem(id, shoppingItemDto);
            return Ok(changedItem);
        }

        [HttpPut]
        [Route("DeleteItem")]
        public IActionResult DeleteItem(Guid id)
        {
            return Ok(_repository.DeleteItem(id));
        }
    }
}
