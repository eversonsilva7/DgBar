using BarDoDG.Application.DTOs;
using BarDoDG.WebSite.Models.Comanda;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BarDoDG.WebSite.Controllers
{
    public class ComandaController : WebBaseController
    {
        public ComandaController(IConfiguration configuration) : base(configuration) { }        

        // GET: ComandaController
        public async Task<IActionResult> ComandasAtivas()
        {
            ComandasAtivasViewModel objResposta = new ComandasAtivasViewModel()
            {                
                LstComandaAtiva = await this.GenericLista<ComandaDTO>(UrlAPI, "v1/Comandas/Ativas")
            };

            return View("~/Views/Comanda/_ComandasAtivas.cshtml", objResposta);
        }

        public async Task<IActionResult> Historico()
        {
            HistoricoComandasViewModel objResposta = new HistoricoComandasViewModel()
            {
                LstHistoricoComanda = await this.GenericLista<ComandaDTO>(UrlAPI, "v1/Comandas/Fechadas")
            };

            return View("~/Views/Comanda/_HistoricoComandas.cshtml", objResposta);
        }

        public IActionResult NovaComanda()
        {
            ComandaViewModel objResposta = new ComandaViewModel();

            return View("~/Views/Comanda/NovaComanda.cshtml", objResposta);
        }

        // POST: ComandaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InserirComanda(ComandaInsertDTO args)
        {
            ComandaViewModel objResposta = new ComandaViewModel();
            int idComanda = 0;

            try
            {                
                HTTPResponse objResponse = null;

                if (string.IsNullOrWhiteSpace(args.NomeCliente))
                {
                    objResposta.Mensagem = "Nome é obrigatório!";
                    return View("~/Views/Comanda/NovaComanda.cshtml", objResposta);
                }

                objResponse = await Post(UrlAPI, $"v1/Comandas", args);

                if (!objResponse.IsSuccessStatusCode)
                {
                    objResposta.Mensagem = objResponse.GetMessage();
                }
                else
                    idComanda = objResponse.GetData<int>();

                objResposta.Status = (int)objResponse.StatusCode;

                return RedirectToRoute(new { Controller = "Comanda", Action = "Detalhes", id = idComanda });
            }
            catch (Exception ex)
            {
                objResposta.Mensagem = ex.Message;
                objResposta.Status = (int)HttpStatusCode.InternalServerError;                
            }

            return View("~/Views/Comanda/NovaComanda.cshtml", objResposta);
        }

        // GET: ComandaController/Details/5
        public async Task<ActionResult> Detalhes(int id)
        {
            DetalhesComandaViewModel objResposta = new DetalhesComandaViewModel();            

            try
            {
                objResposta.ComandaDTO = await this.GenericSelecionar<ComandaDTO>(UrlAPI, $"v1/Comandas/{id}");
                objResposta.LstItem = await this.GenericLista<ItemDTO>(UrlAPI, $"v1/Itens");
            }
            catch (Exception ex)
            {
                objResposta.Mensagem = ex.Message;
                objResposta.Status = (int)HttpStatusCode.InternalServerError;
            }

            return View("~/Views/Comanda/DetalhesComanda.cshtml", objResposta);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FecharComanda(ComandaDTO args)
        {
            DetalhesComandaViewModel objResposta = new DetalhesComandaViewModel();
            
            try
            {
                HTTPResponse objResponse = null;

                if (args.IdComanda <= 0)
                {
                    objResposta.Mensagem = "Comanda é obrigatória!";
                    objResposta.ComandaDTO = await this.GenericSelecionar<ComandaDTO>(UrlAPI, $"v1/Comandas/{args.IdComanda}");
                    objResposta.LstItem = await this.GenericLista<ItemDTO>(UrlAPI, $"v1/Itens");
                    return View("~/Views/Comanda/DetalhesComanda.cshtml", objResposta);
                }

                objResponse = await Patch(UrlAPI, $"v1/Comandas/{args.IdComanda}", null);

                if (!objResponse.IsSuccessStatusCode)
                {
                    objResposta.Mensagem = objResponse.GetMessage();
                }
                else
                    return RedirectToRoute(new { Controller = "Comanda", Action = "Historico" });

                objResposta.Status = (int)objResponse.StatusCode;
            }
            catch (Exception ex)
            {
                objResposta.Mensagem = ex.Message;
                objResposta.Status = (int)HttpStatusCode.InternalServerError;
            }

            return View("~/Views/Comanda/DetalhesComanda.cshtml", objResposta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InserirItemComanda(ItemCompradoDTO args)
        {
            DetalhesComandaViewModel objResposta = new DetalhesComandaViewModel();
            int id = 0;
            try
            {
                HTTPResponse objResponse = null;

                if (args.IdComanda <= 0 || args.IdItem <= 0)
                {
                    objResposta.Mensagem = "Comanda e item são obrigatórios!";
                    objResposta.ComandaDTO = await this.GenericSelecionar<ComandaDTO>(UrlAPI, $"v1/Comandas/{args.IdComanda}");
                    objResposta.LstItem = await this.GenericLista<ItemDTO>(UrlAPI, $"v1/Itens");
                    return View("~/Views/Comanda/DetalhesComanda.cshtml", objResposta);
                }

                objResponse = await Post(UrlAPI, $"v1/ItensComprados", args);

                if (!objResponse.IsSuccessStatusCode)
                {
                    objResposta.Mensagem = objResponse.GetMessage();
                }
                else
                {
                    id = objResponse.GetData<int>();

                    objResposta.Status = (int)objResponse.StatusCode;
                    objResposta.Mensagem = "Item adicionado na comanda com sucesso.";
                    return RedirectToRoute(new { Controller = "Comanda", Action = "Detalhes", id = args.IdComanda });
                }
            }
            catch (Exception ex)
            {
                objResposta.Mensagem = ex.Message;
                objResposta.Status = (int)HttpStatusCode.InternalServerError;
            }

            objResposta.ComandaDTO = await this.GenericSelecionar<ComandaDTO>(UrlAPI, $"v1/Comandas/{args.IdComanda}");
            objResposta.LstItem = await this.GenericLista<ItemDTO>(UrlAPI, $"v1/Itens");

            return View("~/Views/Comanda/DetalhesComanda.cshtml", objResposta);
        }

        [HttpPost]        
        public async Task<ActionResult> DeletarItemComprado(ItemCompradoDTO args)
        {
            DetalhesComandaViewModel objResposta = new DetalhesComandaViewModel();

            try
            {
                HTTPResponse objResponse = null;

                if (args.IdComanda <= 0 || args.IdItemComprado <= 0)
                {
                    objResposta.Mensagem = "Comanda e o item são obrigatórios!";
                }
                else
                {
                    objResponse = await Delete(UrlAPI, $"v1/ItensComprados/{args.IdItemComprado}", args.IdItemComprado.ToString(), null);

                    if (!objResponse.IsSuccessStatusCode)
                    {
                        objResposta.Mensagem = objResponse.GetMessage();
                    }
                    else
                        return RedirectToRoute(new { Controller = "Comanda", Action = "Detalhes", id = args.IdComanda });

                    objResposta.Status = (int)objResponse.StatusCode;
                }
            }
            catch (Exception ex)
            {
                objResposta.Mensagem = ex.Message;
                objResposta.Status = (int)HttpStatusCode.InternalServerError;
            }

            //objResposta.ComandaDTO = await this.GenericSelecionar<ComandaDTO>(UrlAPI, $"v1/Comandas/{args.IdComanda}");
            //objResposta.LstItem = await this.GenericLista<ItemDTO>(UrlAPI, $"v1/Itens");

            return View("~/Views/Comanda/DetalhesComanda.cshtml", objResposta);
        }
    }
}
