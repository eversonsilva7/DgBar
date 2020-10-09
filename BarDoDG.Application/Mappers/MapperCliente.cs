using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BarDoDG.Application.Mappers
{
    public class MapperCliente : IMapperCliente
    {        
        public Cliente MapperDTOToEntity(ClienteDTO clienteDTO)
        {
            Cliente cliente = new Cliente()
            {
                IdCliente = clienteDTO.IdCliente,
                Nome = clienteDTO.Nome
            };

            return cliente;
        }

        public ClienteDTO MapperEntityToDTO(Cliente cliente)
        {
            ClienteDTO clienteDTO = new ClienteDTO()
            {
                IdCliente = cliente.IdCliente,
                Nome = cliente.Nome
            };

            return clienteDTO;
        }

        public IEnumerable<ClienteDTO> MapperListClientesDTO(IEnumerable<Cliente> clientes)
        {
            var dtos = clientes.Select(c => new ClienteDTO { IdCliente = c.IdCliente, Nome = c.Nome });
            return dtos;
        }
    }
}
