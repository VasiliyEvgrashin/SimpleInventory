using Inventory.Helpers.Interfaces;
using Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        IBalanceHelper _balanceHelper;

        public BalanceController(IBalanceHelper balanceHelper)
        {
            _balanceHelper = balanceHelper;
        }

        [HttpPost]
        public async Task<IEnumerable<BalanceModel>> Get([FromBody] BalanceFilter filter) => await _balanceHelper.Get(filter);
    }
}
