using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Application.Validation;
using BarDoDG.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace BarDoDG.Application
{
    public class ApplicationServiceCliente : IApplicationServiceCliente
    {
        private readonly IServiceCliente _serviceCliente;
        private readonly IMapperCliente _mapperCliente;

        public ApplicationServiceCliente(IServiceCliente serviceCliente, IMapperCliente mapperCliente)
        {
            _serviceCliente = serviceCliente;
            _mapperCliente = mapperCliente;
        }

        public void Add(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
            {
                throw new ObjectNotFoundException("Objeto não informado.");
            }
            else
            {
                var cliente = _mapperCliente.MapperDTOToEntity(clienteDTO);
                _serviceCliente.Add(cliente);
            }
        }

        public int Insert(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
            {
                throw new ObjectNotFoundException("Objeto não informado.");
            }
            else
            {
                var cliente = _mapperCliente.MapperDTOToEntity(clienteDTO);
                return _serviceCliente.Insert(cliente);
            }
        }

        public IEnumerable<ClienteDTO> GetAll()
        {
            //recupera a lista de clientes e retorna uma lista em DTO
            var clientes = _serviceCliente.GetAll();
            return _mapperCliente.MapperListClientesDTO(clientes);
        }

        public ClienteDTO GetById(int id)
        {
            var cliente = _serviceCliente.GetById(id);
            if (cliente == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");
            else
                return _mapperCliente.MapperEntityToDTO(cliente);
        }

        public void Delete(int id)
        {
            var cliente = _serviceCliente.GetById(id);
            if (cliente == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");
            else
            {
                _serviceCliente.Delete(id);
            }
        }

        public void Update(ClienteDTO clienteDTO)
        {
            var cliente = _serviceCliente.GetById(clienteDTO.IdCliente);
            if (cliente == null)
                throw new ObjectNotFoundException("Objeto não encontrado.");
            else
            {
                cliente = _mapperCliente.MapperDTOToEntity(clienteDTO);
                _serviceCliente.Update(cliente);
            }
        }
    }
}
