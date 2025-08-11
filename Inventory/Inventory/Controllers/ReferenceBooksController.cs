using Inventory.Core;
using Inventory.DB.References;
using Inventory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : GenericCrud<Client>
    {
        public ClientController(IRepository repository)
            : base(repository)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasurementController : GenericCrud<UnitOfMeasurement>
    {
        public UnitOfMeasurementController(IRepository repository)
            : base(repository)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : GenericCrud<Resource>
    {
        public ResourceController(IRepository repository)
            : base(repository)
        {
        }
    }
}
