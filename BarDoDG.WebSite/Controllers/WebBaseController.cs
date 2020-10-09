using BarDoDG.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BarDoDG.WebSite.Controllers
{
    /// <summary>
    /// Controller padrão para herança dos projetos MVC
    /// </summary>
    public abstract class WebBaseController : Controller
    {
        //URL API (AppSettings)
        protected static string UrlAPI { get; private set; }

        protected static IConfiguration _configuration;

        public WebBaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlAPI = configuration["appConfiguration:API:URL"];
        }

        #region Verbos HTTP
        /// <summary>
        /// Executa requisições GET (ASync) conforme solicitado...
        /// </summary>
        /// <param name="baseAddress">Endereço base do microserviço;</param>
        /// <param name="uri">caminho (rota) do recurso solicitado;</param>
        /// <param name="token">token JWT.IO (opcional);</param>
        /// <param name="requestHeaders">informações via headers (opcional);</param>
        /// <returns>Objeto do tipo HTTPResponse com o conteúdo da mensagem...</returns>
        protected static async Task<HTTPResponse> Get(string baseAddress, string uri, string token = null, HttpHeaders requestHeaders = null)
        {
            using (HttpClient objClient = new HttpClient())
            {
                objClient.BaseAddress = new Uri(baseAddress);

                HandleHeader(objClient, requestHeaders);

                using (var _response = await objClient.GetAsync(uri))
                {
                    return await HandleAnswer(_response);
                }
            }
        }

        protected static async Task<HTTPResponse> Get(string baseAddress, string uri, object args, string token = null, HttpHeaders requestHeaders = null)
        {
            using (HttpClient objClient = new HttpClient())
            {
                HandleHeader(objClient, requestHeaders);

                using (var _response = await CustomAsync(HttpMethod.Get, objClient, new Uri(baseAddress + uri), args != null ? new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json") : new StringContent("")))
                {
                    return await HandleAnswer(_response);
                }
            }
        }

        /// <summary>
        /// Executa requisições POST (ASync) conforme solicitado...
        /// </summary>
        /// <param name="baseAddress">Endereço base do microserviço;</param>
        /// <param name="uri">caminho (rota) do recurso solicitado;</param>
        /// <param name="args">Objeto que será serializado e enviado na requisição;</param>
        /// <param name="token">token JWT.IO (opcional);</param>
        /// <param name="requestHeaders">informações via headers (opcional);</param>
        /// <returns>Objeto do tipo HTTPResponse com o conteúdo da mensagem...</returns>
        protected static async Task<HTTPResponse> Post(string baseAddress, string uri, object args, string token = null, HttpHeaders requestHeaders = null)
        {
            using (HttpClient objClient = new HttpClient())
            {
                objClient.BaseAddress = new Uri(baseAddress);                
                HandleHeader(objClient, requestHeaders);

                using (var _response = await objClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json")))
                {
                    return await HandleAnswer(_response);
                }
            }
        }

        /// <summary>
        /// Executa requisições DELETE (Async) conforme solicitado...
        /// </summary>
        /// <param name="baseAddress">Endereço base do microserviço;</param>
        /// <param name="uri">caminho (rota) do recurso solicitado;</param>
        /// <param name="args">Objeto que será serializado e enviado na requisição;</param>
        /// <param name="token">token JWT.IO (opcional);</param>
        /// <param name="requestHeaders">informações via headers (opcional);</param>
        /// <returns>Objeto do tipo HTTPResponse com o conteúdo da mensagem...</returns>
        protected static async Task<HTTPResponse> Delete(string baseAddress, string uri, string id, string token = null, HttpHeaders requestHeaders = null)
        {
            using (HttpClient objClient = new HttpClient())
            {
                objClient.BaseAddress = new Uri(baseAddress);                
                HandleHeader(objClient, requestHeaders);

                using (var _response = await objClient.DeleteAsync($"{baseAddress}{uri}?id={id}"))
                {
                    return await HandleAnswer(_response);
                }
            }
        }

        /// <summary>
        /// Executa requisições PATCH (Async) conforme solicitado...
        /// </summary>
        /// <param name="baseAddress">Endereço base do microserviço;</param>
        /// <param name="uri">caminho (rota) do recurso solicitado;</param>
        /// <param name="args">Objeto que será serializado e enviado na requisição;</param>
        /// <param name="token">token JWT.IO (opcional);</param>
        /// <param name="requestHeaders">informações via headers (opcional);</param>
        /// <returns>Objeto do tipo HTTPResponse com o conteúdo da mensagem...</returns>
        protected static async Task<HTTPResponse> Patch(string baseAddress, string uri, object args, string token = null, HttpHeaders requestHeaders = null)
        {
            using (HttpClient objClient = new HttpClient())
            {                
                HandleHeader(objClient, requestHeaders);

                using (var _response = await CustomAsync(new HttpMethod("PATCH"), objClient, new Uri(baseAddress + uri), args != null ? new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json") : new StringContent("")))
                {
                    return await HandleAnswer(_response);
                }
            }
        }

        protected static async Task<HttpResponseMessage> CustomAsync(HttpMethod verb, HttpClient client, Uri requestUri, HttpContent iContent)
        {
            using (var request = new HttpRequestMessage(verb, requestUri)
            {
                Content = iContent
            })
            {
                try
                {
                    return await client.SendAsync(request);
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = ex.Message };
                }
            }
        }

        /// <summary>
        /// Executa requisições PUT (ASync) conforme solicitado...
        /// </summary>
        /// <param name="baseAddress">Endereço base do microserviço;</param>
        /// <param name="uri">caminho (rota) do recurso solicitado;</param>
        /// <param name="args">Objeto que será serializado e enviado na requisição;</param>
        /// <param name="token">token JWT.IO (opcional);</param>
        /// <returns>Objeto do tipo HTTPResponse com o conteúdo da mensagem...</returns>
        protected static async Task<HTTPResponse> Put(string baseAddress, string uri, object args, string token = null)
        {
            using (HttpClient objClient = new HttpClient())
            {
                objClient.BaseAddress = new Uri(baseAddress);                

                using (var _response = await objClient.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json")))
                {
                    return await HandleAnswer(_response);
                }
            }
        }
        #endregion

        #region [Métodos internos da classe]
        /// <summary>
        /// Executa requisições via headers
        /// </summary>
        /// <param name="obj">Client HTTP utilizado na requisição;</param>
        /// <param name="requestHeaders">headers</param>
        /// <returns>Objeto do tipo HTTPResponse com o conteúdo da mensagem...</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void HandleHeader(HttpClient obj, HttpHeaders requestHeaders = null)
        {
            if (requestHeaders != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in requestHeaders)
                {
                    obj.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Compõe o objeto de retorno de uma requisição...
        /// </summary>
        /// <param name="pResponseMessage"></param>
        /// <returns></returns>
        internal static async Task<HTTPResponse> HandleAnswer(HttpResponseMessage pResponseMessage)
        {
            HTTPResponse obj = new HTTPResponse(pResponseMessage.StatusCode);

            if (pResponseMessage.IsSuccessStatusCode)
            {
                if (obj.StatusCode != HttpStatusCode.NoContent)
                {
                    obj.Data = await pResponseMessage.Content.ReadAsStringAsync();
                }
            }
            else
            {
                obj.Data = await pResponseMessage.Content.ReadAsStringAsync();
            }

            obj.Headers = pResponseMessage.Headers;

            return obj;
        }
        #endregion

        #region Métodos genéricos para chamada GET
        /// <summary>
        /// Retorna dados base para DropDownList(combo), padrão do sistema...
        /// </summary>
        /// <typeparam name="TType">Tipo de Dado que será retornado;</typeparam>
        /// <param name="API">URL Base da API (microserviço) em que a ação está hospedada;</param>
        /// <param name="URL">URL da ação (GET) que será invocada;</param>        
        /// <returns>List de <typeparamref name="TType"/>...</returns>
        public async Task<List<TType>> GenericLista<TType>(string API, string URL)
        {
            HTTPResponse objResponse = null;  // Dados da requisição...
            List<TType> lstRetorno = null;

            objResponse = await Get(API, URL);

            if (!objResponse.IsSuccessStatusCode)
            {
                throw new HttpStatusCodeException((int)objResponse.StatusCode, objResponse.GetMessage());
            }
            else
            {
                lstRetorno = objResponse.GetData<List<TType>>();
            }

            return lstRetorno;
        }

        /// <summary>
        /// Retorna dados base do DTO informado
        /// </summary>
        /// <typeparam name="TType">Tipo de Dado que será retornado;</typeparam>
        /// <param name="API">URL Base da API (microserviço) em que a ação está hospedada;</param>
        /// <param name="URL">URL da ação (GET) que será invocada;</param>        
        /// <returns>Objeto <typeparamref name="TType"/>...</returns>
        public async Task<TType> GenericSelecionar<TType>(string API, string URL, HttpHeaders headers = null)
        {
            HTTPResponse objResponse = null;  // Dados da requisição...
            TType retorno;

            objResponse = await Get(API, URL, headers);

            if (!objResponse.IsSuccessStatusCode)
            {
                throw new HttpStatusCodeException((int)objResponse.StatusCode, objResponse.GetMessage());
            }
            else
            {
                retorno = objResponse.GetData<TType>();
            }

            return retorno;
        }
        #endregion
    }

    /// <summary>
    /// Classe padrão de retorno das comunicações HTTP...
    /// </summary>
    public class HTTPResponse
    {
        private readonly int _intStatusCode;

        #region [Propriedades]
        /// <summary>
        /// Código de Status do retorno da comunicação
        /// <see cref="HttpStatusCode"/>
        /// </summary>
        public HttpStatusCode StatusCode { get { return (HttpStatusCode)_intStatusCode; } }

        /// <summary>
        /// Conteúdo serializado da resposta com sucesso ou mensagem (<see cref="HttpResponseMessage.ReasonPhrase"/> para mais detalhes)...
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Headers de resposta da solicitação...
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// Headers de envio na solicitação...
        /// </summary>
        public HttpRequestHeaders HeadersRequest { get; set; }

        /// <summary>
        /// Returna true, para StatusCode entre 200 e 299...
        /// </summary>
        public bool IsSuccessStatusCode
        {
            get { return ((int)StatusCode >= 200 && (int)StatusCode <= 299); }
        }
        #endregion

        #region [Método]
        public HTTPResponse(HttpStatusCode StatusCode)
        {
            _intStatusCode = (int)StatusCode;
        }

        /// <summary>
        /// Obtém o tipo de dado composto pela desserialização da propriedade DATA
        /// </summary>
        /// <typeparam name="TTipo">Tipo de dado que será utilizado na deserialização</typeparam>
        /// <returns>Objeto do tipo TTipo</returns>
        public TTipo GetData<TTipo>()
        {
            if (((int)StatusCode >= 200 && (int)StatusCode <= 299) && (StatusCode != HttpStatusCode.NoContent))
                return JsonConvert.DeserializeObject<TTipo>(Data);
            else
                return Activator.CreateInstance<TTipo>();
        }

        public string GetMessage()
        {
            if ((int)StatusCode > 299)
            {
                if (StatusCode == HttpStatusCode.Unauthorized)
                {
                    return "Sua sessão expirou. Faça o login novamente.";
                }
                else
                    return JsonConvert.DeserializeObject<APIBaseResponse>(Data)?.Message;
            }
            else
                return null;
        }
        #endregion
    }
}
