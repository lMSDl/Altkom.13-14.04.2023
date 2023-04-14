using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        private readonly ICollection<T> _entities;

        public EntityService(EntityFaker<T> faker, int count)
        {
            _entities = faker.Generate(count).ToList();
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(_entities.SingleOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(_entities.ToList().AsEnumerable());
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            if (entity != null)
                _entities.Remove(entity);
        }

        public Task<int> CreateAsync(T entity)
        {
            entity.Id = _entities.Max(x => x.Id) + 1;
            _entities.Add(entity);

            return Task.FromResult(entity.Id);
        }
    }
}