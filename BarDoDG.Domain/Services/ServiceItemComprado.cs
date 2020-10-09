using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Domain.Services
{
    public class ServiceItemComprado : ServiceBase<ItemComprado>, IServiceItemComprado
    {
        private readonly IRepositoryItemComprado _repositoryItemComprado;

        public ServiceItemComprado(IRepositoryItemComprado repositoryItemComprado) : base(repositoryItemComprado)
        {
            _repositoryItemComprado = repositoryItemComprado;
        }
        public int Insert(ItemComprado itemComprado)
        {
            return _repositoryItemComprado.Insert(itemComprado);
        }

        public List<ItemComprado> GetAllByComanda(int idComanda)
        {
            return _repositoryItemComprado.GetAll()?.Where(p => p.IdComanda == idComanda)?.ToList();
        }
    }
}
