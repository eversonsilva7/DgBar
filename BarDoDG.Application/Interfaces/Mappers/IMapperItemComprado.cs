using BarDoDG.Application.DTOs;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces.Mappers
{
    //Responsável pelo mapeamento do DTO para entidade
    public interface IMapperItemComprado
    {
        ItemComprado MapperDTOToEntity(ItemCompradoDTO ItemCompradoDTO);
        IEnumerable<ItemCompradoDTO> MapperListItemCompradosDTO(IEnumerable<ItemComprado> itensComandas);
        ItemCompradoDTO MapperEntityToDTO(ItemComprado ItemComprado);
    }
}
