using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.Application.Interfaces
{
    public interface IApplicationServiceCliente
    {
        void Add(ClienteDTO clienteDTO);
        int Insert(ClienteDTO clienteDTO);
        void Update(ClienteDTO clienteDTO);
        void Delete(int id);
        IEnumerable<ClienteDTO> GetAll();
        ClienteDTO GetById(int id);
    }
}
