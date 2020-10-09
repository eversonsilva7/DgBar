using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace BarDoDG.Domain.Services
{
    public class ServiceComanda : ServiceBase<Comanda>, IServiceComanda
    {
        private readonly IRepositoryComanda _repositoryComanda;

        public ServiceComanda(IRepositoryComanda repositoryComanda) : base(repositoryComanda)
        {
            this._repositoryComanda = repositoryComanda;
        }

        public int Insert(Comanda comanda)
        {
            return _repositoryComanda.Insert(comanda);
        }
    }
}
