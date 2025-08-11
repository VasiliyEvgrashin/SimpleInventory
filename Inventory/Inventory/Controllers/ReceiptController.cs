using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        IRptRepository _repository;

        public ReceiptController(IRptRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ReceiptListModel>> Get(DateTime datefrom, DateTime dateto) => await _repository.GetAsync(datefrom, dateto);
    }
}
