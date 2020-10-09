using BarDoDG.Domain.Entities;

namespace BarDoDG.Domain.Interfaces.Repositories
{
    public interface IRepositoryComanda : IRepositoryBase<Comanda>
    {
        int Insert(Comanda comanda);
    }
}
