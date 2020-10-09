using BarDoDG.Application.DTOs;
using System.Collections.Generic;

namespace BarDoDG.WebSite.Models.Cliente
{
    public class ClientesViewModel : ViewBaseModel
    {
        public List<ClienteDTO> LstCliente { get; set; }

        public ClientesViewModel()
        {
            LstCliente = new List<ClienteDTO>();
        }
    }
}
