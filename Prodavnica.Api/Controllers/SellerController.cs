using Microsoft.AspNetCore.Mvc;
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
    }
}
