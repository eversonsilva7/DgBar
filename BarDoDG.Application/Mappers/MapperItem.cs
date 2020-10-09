using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Application.Mappers
{
    public class MapperItem : IMapperItem
    {        
        public Item MapperDTOToEntity(ItemDTO itemDTO)
        {
            Item Item = new Item()
            {
                IdItem = itemDTO.IdItem,
                Descricao = itemDTO.Descricao,
                Valor = itemDTO.Valor
            };

            return Item;
        }

        public ItemDTO MapperEntityToDTO(Item item)
        {
            ItemDTO ItemDTO = new ItemDTO()
            {
                IdItem = item.IdItem,
                Descricao = item.Descricao,
                Valor = item.Valor
            };

            return ItemDTO;
        }

        public IEnumerable<ItemDTO> MapperListItemsDTO(IEnumerable<Item> Items)
        {
            var dtos = Items.Select(c => new ItemDTO { IdItem = c.IdItem, Descricao = c.Descricao, Valor = c.Valor });
            return dtos;
        }
    }
}
