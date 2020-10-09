using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace BarDoDG.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ItensCompradosController : APIBaseController
    {
        private readonly IApplicationServiceItemComprado _applicationServiceItemComprado;

        public ItensCompradosController(IApplicationServiceItemComprado applicationServiceItemComprado)
        {
            _applicationServiceItemComprado = applicationServiceItemComprado;
        }

        /// <summary>
        /// Lista todos os itens comprados
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllItensComprados()
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceItemComprado.GetAll());
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Lista todos os itens comprados nessa comanda
        /// </summary>
        /// <param name="idComanda"></param>
        /// <returns></returns>        
        [HttpGet("{id}")]
        public ActionResult GetAllItemCompradoByComanda(int idComanda)
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceItemComprado.GetAllByComanda(idComanda));
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }
                
        /// <summary>
        /// Gravar um item na comanda
        /// </summary>
        /// <param name="itemCompradoDTO"></param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult Post([FromBody] ItemCompradoDTO itemCompradoDTO)
        {
            try
            {
                if (itemCompradoDTO == null)
                    return ReturnAPI(HttpStatusCode.BadRequest, "Parâmetros obrigatórios não informados.");

                int id = _applicationServiceItemComprado.Insert(itemCompradoDTO);
                return ReturnAPI(HttpStatusCode.Created, id);
            }
            catch (ObjectNotFoundException ex)
            {
                return ReturnAPI(HttpStatusCode.NotFound, ex);
            }
            catch (BadRequestException ex)
            {
                return ReturnAPI(HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        /// <summary>
        /// Deletar um item da comanda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return ReturnAPI(HttpStatusCode.BadRequest);

                _applicationServiceItemComprado.Delete(id);
                return ReturnAPI(HttpStatusCode.NoContent);
            }
            catch (ObjectNotFoundException ex)
            {
                return ReturnAPI(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
