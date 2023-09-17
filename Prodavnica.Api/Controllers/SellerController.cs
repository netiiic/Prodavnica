using Microsoft.AspNetCore.Mvc;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Interfaces;

namespace Prodavnica.Api.Controllers
{
    [Route("api/seller")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _service;

        public SellerController(ISellerService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("AddNewItem")]
        public IActionResult AddNewItem([FromBody] ShoppingItemDto shoppingItemDto)
        {
            return Ok(_service.AddNewItemToDB(shoppingItemDto));
        }

        [HttpPut]
        [Route("GetAllMyItems")]
        public IActionResult GetAllMyItems(Guid sellerId)
        {
            List<ShoppingItemDto> ret = new();
            ret = _service.GetAllMyItems(sellerId);
            if (ret == null)
            {
                return BadRequest("You don't exist.");
            }
            else
            {
                return Ok(ret);
            }
        }

        [HttpPut]
        [Route("UpdateItem")]
        public IActionResult UpdateItem(Guid id, [FromBody] ShoppingItemDto shoppingItemDto)
        {
            return Ok(_service.UpdateItem(id, shoppingItemDto));
        }

        [HttpPut]
        [Route("DeleteItem")]
        public IActionResult DeleteItem(Guid id)
        {
            return Ok(_service.DeleteItem(id));
        }

        [HttpGet]
        [Route("GetMyOrders")]
        public IActionResult GetMyOrders(Guid sellerId)
        {
            List<OrederDto> ret = new();
            ret = _service.GetSellerOrders(sellerId);
            if(ret == null)
            {
                return BadRequest("You don't exist.");
            }
            else 
            {
                return Ok(ret);
            }
            
        }
    }
}
