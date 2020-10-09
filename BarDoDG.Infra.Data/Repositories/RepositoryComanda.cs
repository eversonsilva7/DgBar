using BarDoDG.Application.DTOs;
using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace BarDoDG.Infra.Data.Repositories
{
    public class RepositoryComanda : RepositoryBase<Comanda>, IRepositoryComanda
    {
        private readonly BARDGContext _dbContext;

        public RepositoryComanda(BARDGContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Insert(Comanda comanda)
        {
            try
            {
                _dbContext.Add(comanda);
                _dbContext.SaveChanges();
                return comanda.IdComanda;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
