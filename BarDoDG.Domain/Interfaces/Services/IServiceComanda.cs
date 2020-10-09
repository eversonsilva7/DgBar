using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Domain.Interfaces.Services
{
    public interface IServiceComanda : IServiceBase<Comanda>
    {
        int Insert(Comanda comanda);       
    }
}
