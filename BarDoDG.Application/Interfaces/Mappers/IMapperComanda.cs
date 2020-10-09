using BarDoDG.Application.DTOs;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces.Mappers
{
    //Responsável pelo mapeamento do DTO para entidade
    public interface IMapperComanda
    {
        Comanda MapperDTOToEntity (ComandaDTO comandaDTO);
        IEnumerable<ComandaDTO> MapperListComandasDTO(IEnumerable<Comanda> comandas);
        ComandaDTO MapperEntityToDTO(Comanda comanda);
    }
}
