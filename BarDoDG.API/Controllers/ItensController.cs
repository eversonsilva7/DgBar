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
    public class ItensController : APIBaseController
    {
        private readonly IApplicationServiceItem _applicationServiceItem;

        public ItensController(IApplicationServiceItem applicationServiceItem)
        {
            _applicationServiceItem = applicationServiceItem;
        }

        /// <summary>
        /// Lista todos os itens
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return ReturnAPI(HttpStatusCode.OK, _applicationServiceItem.GetAll());
            }
            catch (Exception ex)
            {
                return ReturnAPI(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
