using Inventory.Helpers.Interfaces;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        IShipmentRepository _repository;
        ICheckBalanceHelper _checkBalanceHelper;

        public ShipmentController(
            IShipmentRepository repository,
            ICheckBalanceHelper checkBalanceHelper)
        {
            _repository = repository;
            _checkBalanceHelper = checkBalanceHelper;
        }

        [HttpGet]
        public async Task<IEnumerable<ShipmentListModel>> Get(DateTime datefrom, DateTime dateto) => await _repository.Get(datefrom, dateto);

        [HttpGet("{id}")]
        public async Task<ShipmentEditModel> Get(int id) => await _repository.Get(id);

        [HttpPost]
        public async Task<ShipmentEditModel> UpSert([FromBody] ShipmentEditModel model) => await _repository.UpSert(model);

        [HttpPost("checkbalance")]
        public async Task<bool> CheckBalance([FromBody] ShipmentEditModel model) => await _checkBalanceHelper.CheckBalance(model);
    }
}
