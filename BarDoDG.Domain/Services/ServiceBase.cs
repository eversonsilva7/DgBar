using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace BarDoDG.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj); 
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
    }
}
