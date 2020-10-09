using System.Collections.Generic;

namespace BarDoDG.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
        IEnumerable<TEntity> GetAll();        
        TEntity GetById(int id);
    }
}
