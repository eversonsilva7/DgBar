using BarDoDG.Domain.Entities;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Domain.Interfaces.Services;

namespace BarDoDG.Domain.Services
{
    public class ServiceCliente : ServiceBase<Cliente>, IServiceCliente
    {
        private readonly IRepositoryCliente _repositoryCliente;

        public ServiceCliente(IRepositoryCliente repositoryCliente) : base(repositoryCliente)
        {
            this._repositoryCliente = repositoryCliente;
        }

        public int Insert(Cliente cliente)
        {
            return _repositoryCliente.Insert(cliente);
        }
    }
}
