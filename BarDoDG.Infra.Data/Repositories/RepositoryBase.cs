using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly BARDGContext _dbContext;

        public RepositoryBase(BARDGContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity obj)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Delete(int id)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(Select(id));
                _dbContext.SaveChanges();                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TEntity obj)
        {
            try
            {
                _dbContext.Entry(obj).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual TEntity Select(int id) => _dbContext.Set<TEntity>().Find(id);
    }
}
