using Inventory.DB.References;
using Inventory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Core
{
    public abstract class GenericCrud<T> : ControllerBase
        where T : BaseReference
    {
        IRepository _repository;

        protected GenericCrud(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<T>> Get() => await _repository.GetAsync<T>();

        [HttpGet("{id}")]
        public async Task<T> Get(int id) => await _repository.GetAsync<T>(id);

        [HttpPost]
        public async Task<int> Post([FromBody] T value) => await _repository.InsertAsync(value);

        [HttpPut()]
        public async Task Put([FromBody] T value) => await _repository.UpdateAsync(value);
    }
}
