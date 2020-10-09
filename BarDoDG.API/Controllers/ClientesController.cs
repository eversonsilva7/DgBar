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
    public class ClientesController : APIBaseController
    {
        private readonly IApplicationServiceCliente _applicationServiceCliente;

        public ClientesController(IApplicationServiceCliente applicationServiceCliente)
        {
            _applicationServiceCliente = applicationServiceCliente;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        /// <returns>Lista todos os clientes</returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceCliente.GetAll());
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }

        #region Não serão necessários
        //[HttpGet("{id}")]
        //public ActionResult<string> GetById(int id)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //            return ReturnAPI(HttpStatusCode.BadRequest);

        //        return ReturnAPI(HttpStatusCode.OK, _applicationServiceCliente.GetById(id));
        //    }
        //    catch (ObjectNotFoundException ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.NotFound, ex);
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.BadRequest, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Post([FromBody] ClienteDTO clienteDTO)
        //{
        //    try
        //    {
        //        if (clienteDTO == null)
        //            return ReturnAPI(HttpStatusCode.BadRequest, "Parâmetros obrigatórios não informados.");

        //        int id = _applicationServiceCliente.Insert(clienteDTO);
        //        return ReturnAPI(HttpStatusCode.Created, id);
        //    }
        //    catch (ObjectNotFoundException ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.NotFound, ex);
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.BadRequest, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ReturnAPI(HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        //[HttpPut("{id}")]
        //public ActionResult Put([FromBody] ClienteDTO clienteDTO)
        //{
        //    try
        //    {
        //        if (clienteDTO == null)
        //            return ReturnAPI(HttpStatusCode.BadRequest);

        //        if (clienteDTO.IdCliente <= 0)
        //            return ReturnAPI(HttpStatusCode.BadRequest);

        //        _applicationServiceCliente.Update(clienteDTO);
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

        //        _applicationServiceCliente.Delete(id);
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
        #endregion
    }
}
