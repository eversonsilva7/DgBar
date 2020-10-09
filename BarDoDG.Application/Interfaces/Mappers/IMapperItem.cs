using BarDoDG.Application.DTOs;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces.Mappers
{
    //Responsável pelo mapeamento do DTO para entidade
    public interface IMapperItem
    {
        Item MapperDTOToEntity (ItemDTO itemDTO);
        IEnumerable<ItemDTO> MapperListItemsDTO(IEnumerable<Item> itens);
        ItemDTO MapperEntityToDTO(Item item);
    }
}
