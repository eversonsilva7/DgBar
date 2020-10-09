using BarDoDG.Domain.Entities;

namespace BarDoDG.Domain.Interfaces.Services
{
    public interface IServiceCliente : IServiceBase<Cliente>
    {
        int Insert(Cliente cliente);
    }
}
