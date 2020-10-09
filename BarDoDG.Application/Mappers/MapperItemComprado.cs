using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Application.Mappers
{
    public class MapperItemComprado : IMapperItemComprado
    {        
        public ItemComprado MapperDTOToEntity(ItemCompradoDTO ItemCompradoDTO)
        {
            ItemComprado ItemComprado = new ItemComprado()
            {
                IdItemComprado = ItemCompradoDTO.IdItemComprado,                
                IdItem = ItemCompradoDTO.IdItem,
                IdComanda = ItemCompradoDTO.IdComanda
            };

            return ItemComprado;
        }

        public ItemCompradoDTO MapperEntityToDTO(ItemComprado ItemComprado)
        {
            ItemCompradoDTO ItemCompradoDTO = new ItemCompradoDTO()
            {
                IdItemComprado = ItemComprado.IdItemComprado,
                IdItem = ItemComprado.IdItem,
                IdComanda = ItemComprado.IdComanda
            };

            return ItemCompradoDTO;
        }

        public IEnumerable<ItemCompradoDTO> MapperListItemCompradosDTO(IEnumerable<ItemComprado> ItemComprados)
        {
            var dtos = ItemComprados.Select(c => new ItemCompradoDTO { IdItemComprado = c.IdItemComprado, IdItem = c.IdItem, IdComanda = c.IdComanda });
            return dtos;
        }
    }
}
