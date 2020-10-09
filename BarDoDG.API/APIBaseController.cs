using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BarDoDG.API
{
    public class APIBaseController : ControllerBase
    {
        protected ObjectResult ReturnAPI(HttpStatusCode statusCode, Exception exception)
        {
            return ReturnAPIException(statusCode, exception);
        }

        protected ObjectResult ReturnAPIException(HttpStatusCode statusCode, Exception exception)
        {            
            return StatusCode((int)statusCode, (exception == null) ? null : new APIBaseResponse() { Message = exception.Message.ToString() });
        }

        /// <summary>
        /// Retorno padronizado da API...
        /// </summary>
        /// <param name="statusCode">HTTPStatusCode que será enviado ao solicitante;</param>
        /// <param name="message">Mensagem informativa (opcional);</param>
        /// <param name="invokeMethodEnd">Verificação se invoca o método de LOG de fim do método...</param>
        /// <returns></returns>
        protected ObjectResult ReturnAPI(HttpStatusCode statusCode, object message = null)
        {
            if (((int)statusCode >= 200) && ((int)statusCode <= 299))
            {
                if (message == null)
                {
                    return StatusCode((int)statusCode, null);
                }
                return StatusCode((int)statusCode, message);
            }
            if (statusCode == HttpStatusCode.NotFound)
            {
                return StatusCode((int)statusCode, null);
            }
            else
            {
                return StatusCode((int)statusCode, (message == null) ? null : new APIBaseResponse() { Message = message.ToString() });
            }
        }
    }
}
