using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Application.Mappers
{
    public class MapperComanda : IMapperComanda
    {        
        public Comanda MapperDTOToEntity(ComandaDTO comandaDTO)
        {
            Comanda Comanda = new Comanda()
            {
                IdComanda = comandaDTO.IdComanda,
                DataAbertura = comandaDTO.DataAbertura,
                DataFechamento = comandaDTO.DataFechamento,
                IdCliente = comandaDTO.IdCliente ?? 0,
                ValorTotal = comandaDTO.ValorTotal,
                Desconto = comandaDTO.ValorTotalComDesconto,
                ValorTotalComDesconto = comandaDTO.ValorTotalComDesconto
            };

            return Comanda;
        }

        public ComandaDTO MapperEntityToDTO(Comanda comanda)
        {
            ComandaDTO ComandaDTO = new ComandaDTO()
            {
                IdComanda = comanda.IdComanda,
                DataAbertura = comanda.DataAbertura,
                DataFechamento = comanda.DataFechamento,
                IdCliente = comanda.IdCliente,
                ValorTotal = comanda.ValorTotal,
                Desconto = comanda.Desconto,
                ValorTotalComDesconto = comanda.ValorTotalComDesconto
            };

            return ComandaDTO;
        }

        public IEnumerable<ComandaDTO> MapperListComandasDTO(IEnumerable<Comanda> Comandas)
        {
            var dtos = Comandas.Select(c => new ComandaDTO 
            { 
                IdComanda = c.IdComanda,
                DataAbertura = c.DataAbertura,
                DataFechamento = c.DataFechamento,
                IdCliente = c.IdCliente,
                ValorTotal = c.ValorTotal,
                Desconto = c.Desconto,
                ValorTotalComDesconto = c.ValorTotalComDesconto
            });
            return dtos;
        }
    }
}
