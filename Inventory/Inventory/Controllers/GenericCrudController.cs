
using Inventory.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public abstract class GenericCrudController<T> : ControllerBase
        where T : class
    {
        IRepository _repository;

        protected GenericCrudController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<T>> Get() => await _repository.GetAsync<T>();

        [HttpGet("{id}")]
        public async Task<T> Get(int id) => await _repository.GetAsync<T>(id);

        [HttpPost]
        public async Task<int> Post([FromBody] T value) => await _repository.InsertAsync<T>(value);

        [HttpPut()]
        public async Task Put([FromBody] T value) => await _repository.UpdateAsync<T>(value);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _repository.DeleteAsync(id);
    }
}
