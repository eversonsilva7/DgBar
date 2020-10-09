using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces
{
    public interface IApplicationServiceItem
    {
        void Add(ItemDTO itemDTO);
        void Update(ItemDTO itemDTO);
        void Delete(int id);
        IEnumerable<ItemDTO> GetAll();
        ItemDTO GetById(int id);
    }
}
