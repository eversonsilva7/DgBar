using BarDoDG.Application.DTOs;
using BarDoDG.WebSite.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BarDoDG.WebSite.Controllers
{
    public class ClienteController : WebBaseController
    {
        public ClienteController(IConfiguration configuration) : base(configuration) { }        

        // GET: ClienteController
        public async Task<IActionResult> Index()
        {
            ClientesViewModel objResposta = new ClientesViewModel()
            {                
                LstCliente = await this.GenericLista<ClienteDTO>(UrlAPI, "v1/Clientes")
            };

            return View("~/Views/Cliente/Clientes.cshtml", objResposta);
        }
    }
}
