using BarDoDG.Application.DTOs;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces.Mappers
{
    //Responsável pelo mapeamento do DTO para entidade
    public interface IMapperCliente
    {
        Cliente MapperDTOToEntity (ClienteDTO clienteDTO);
        IEnumerable<ClienteDTO> MapperListClientesDTO(IEnumerable<Cliente> clientes);
        ClienteDTO MapperEntityToDTO(Cliente cliente);
    }
}
