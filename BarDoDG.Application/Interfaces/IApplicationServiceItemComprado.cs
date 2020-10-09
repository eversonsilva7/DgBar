using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces
{
    public interface IApplicationServiceItemComprado
    {        
        int Insert(ItemCompradoDTO ItemCompradoDTO);
        void Delete(int id);
        List<ItemCompradoDTO> GetAllByComanda(int idComanda);
        IEnumerable<ItemCompradoDTO> GetAll();
    }
}
