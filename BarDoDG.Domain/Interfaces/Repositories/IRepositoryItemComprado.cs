using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Domain.Interfaces.Repositories
{
    public interface IRepositoryItemComprado : IRepositoryBase<ItemComprado>
    {
        int Insert(ItemComprado itemComprado);
        List<ItemComprado> GetAllByComanda(int idComanda);
    }
}
