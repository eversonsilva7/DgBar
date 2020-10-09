using BarDoDG.Domain.Entities;

namespace BarDoDG.Domain.Interfaces.Repositories
{
    public interface IRepositoryCliente : IRepositoryBase<Cliente>
    {
        int Insert(Cliente cliente);
    }
}
