using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Infra.Data.Repositories
{
    public class RepositoryItemComprado : RepositoryBase<ItemComprado>, IRepositoryItemComprado
    {
        private readonly BARDGContext _dbContext;

        public RepositoryItemComprado(BARDGContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Insert(ItemComprado itemComprado)
        {
            try
            {
                _dbContext.Add(itemComprado);
                _dbContext.SaveChanges();
                return itemComprado.IdItemComprado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ItemComprado> GetAllByComanda(int idComanda)
        {
            try
            {
                return _dbContext.Set<ItemComprado>().ToList()?.Where(p => p.IdComanda == idComanda)?.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
