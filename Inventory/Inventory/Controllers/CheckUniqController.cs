using Inventory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckUniqController : ControllerBase
    {
        ICheckUniqRepository _repository;

        public CheckUniqController(ICheckUniqRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("receipt")]
        public async Task<bool> CheckUniqueReceipt(string value)
        {
            bool result = await _repository.CheckUniqueReceipt(value);
            return result;
        }

        [HttpGet("shipment")]
        public async Task<bool> CheckUniqueShipment(string value)
        {
            bool result = await _repository.CheckUniqueShipment(value);
            return result;
        }
    }
}
