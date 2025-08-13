using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        IReceiptRepository _repository;

        public ReceiptController(IReceiptRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ReceiptListModel>> Get(DateTime datefrom, DateTime dateto) => await _repository.Get(datefrom, dateto);

        [HttpGet("{id}")]
        public async Task<ReceiptEditModel> Get(int id) => await _repository.Get(id);

        [HttpPost]
        public async Task<ReceiptEditModel> UpSert([FromBody] ReceiptEditModel model) => await _repository.UpSert(model);
    }
}
