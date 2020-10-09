using BarDoDG.Application.DTOs;
using BarDoDG.WebSite.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.WebSite.Controllers
{
    public class HomeController : WebBaseController
    {
        public HomeController(IConfiguration configuration) : base(configuration) { }

        public async Task<IActionResult> Index()
        {
            List<ComandaDTO> LstComandas = await this.GenericLista<ComandaDTO>(UrlAPI, "v1/Comandas");

            HomeViewModel objResposta = new HomeViewModel()
            {
                
                ComandasAtivasViewModel = new Models.Comanda.ComandasAtivasViewModel()
                {
                    LstComandaAtiva = await this.GenericLista<ComandaDTO>(UrlAPI, "v1/Comandas/Ativas")
                },
            };

            objResposta.QuantidadeItensComprados = (await this.GenericLista<ItemCompradoDTO>(UrlAPI, "v1/ItensComprados")).Count;
            objResposta.QuantidadeClientes = (await this.GenericLista<ItemCompradoDTO>(UrlAPI, "v1/Clientes")).Count;

            if (LstComandas?.Count > 0)
            {
                objResposta.QuantidadeComandas = LstComandas.Count;
                objResposta.TotalComandas = (decimal)LstComandas.Sum(p => p.ValorTotalComDesconto);
            }

            return View("~/Views/Home/Index.cshtml", objResposta);
        }
    }
}
