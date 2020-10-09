using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Domain.Interfaces.Services
{
    public interface IServiceItemComprado : IServiceBase<ItemComprado>
    {
        int Insert(ItemComprado itemComprado);
        List<ItemComprado> GetAllByComanda(int idComanda);
    }
}
