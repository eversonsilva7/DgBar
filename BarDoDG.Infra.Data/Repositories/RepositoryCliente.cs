using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Infra.Data.Context;
using System;

namespace BarDoDG.Infra.Data.Repositories
{
    public class RepositoryCliente : RepositoryBase<Cliente>, IRepositoryCliente
    {
        private readonly BARDGContext _dbContext;

        public RepositoryCliente(BARDGContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Insert(Cliente cliente)
        {
            try
            {
                _dbContext.Add(cliente);
                _dbContext.SaveChanges();
                return cliente.IdCliente;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
