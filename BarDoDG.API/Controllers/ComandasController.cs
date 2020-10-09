using BarDoDG.Application.DTOs;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BarDoDG.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ComandasController : APIBaseController
    {
        private readonly IApplicationServiceComanda _applicationServiceComanda;
        private readonly IApplicationServiceItemComprado _applicationServiceItemComprado;

        public ComandasController(IApplicationServiceComanda applicationServiceComanda, IApplicationServiceItemComprado applicationServiceItemComprado)
        {
            _applicationServiceComanda = applicationServiceComanda;
            _applicationServiceItemComprado = applicationServiceItemComprado;
        }

        /// <summary>
        /// Retorna todas as comandas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceComanda.GetAllPersonalized());
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Retorna todas as comandas ativas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Ativas")]
        public ActionResult GetAllComandasActive()
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceComanda.GetAllPersonalized().Where(p => p.DataFechamento == null).ToList());
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Retorna todas as comandas fechadas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Fechadas")]
        public ActionResult GetAllComandasClosed()
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceComanda.GetAllPersonalized().Where(p => p.DataFechamento != null).ToList());
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Retorna a comanda completa com os itens comprados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetPersonalizedByIdWithItens(int id)
        {
            try
            {
                if (id <= 0)
                    return ReturnAPI(HttpStatusCode.BadRequest);

                ComandaDTO comandaDTO = _applicationServiceComanda.GetPersonalizedById(id);
                if (comandaDTO != null)
                {
                    comandaDTO.LstItemComprado = _applicationServiceItemComprado.GetAllByComanda(id);
                }

                return ReturnAPI(HttpStatusCode.OK, comandaDTO);
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
        /// Se o idCliente não estiver cadastrado, mas passar o nome do cliente, será criado um cliente para a comanda
        /// </summary>
        /// <param name="comandaInsertDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] ComandaInsertDTO comandaInsertDTO)
        {
            try
            {
                if (comandaInsertDTO == null)
                    return ReturnAPI(HttpStatusCode.BadRequest, "Parâmetros obrigatórios não informados.");

                int id = _applicationServiceComanda.Insert(comandaInsertDTO);
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
        /// Fechar a comanda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult CloseComanda(int id)
        {
            try
            {
                if (id <= 0)
                    return ReturnAPI(HttpStatusCode.BadRequest);

                _applicationServiceComanda.AtualizarComanda(id, true);
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

        //[HttpPut("{id}")]
        //public ActionResult Put([FromBody] ComandaDTO comandaDTO)
        //{
        //    try
        //    {
        //        if (comandaDTO == null)
        //            return ReturnAPI(HttpStatusCode.BadRequest);

        //        if (comandaDTO.IdComanda <= 0)
        //            return ReturnAPI(HttpStatusCode.BadRequest);

        //        _applicationServiceComanda.Update(comandaDTO);
        //        return ReturnAPI(HttpStatusCode.NoContent);
        //    }
        //    catch (ObjectNotFoundException ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.NotFound, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //            return ReturnAPI(HttpStatusCode.BadRequest);

        //        _applicationServiceComanda.Delete(id);
        //        return ReturnAPI(HttpStatusCode.NoContent);
        //    }
        //    catch (ObjectNotFoundException ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.NotFound, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.InternalServerError, ex);
        //    }
        //}
    }
}
