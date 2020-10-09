using BarDoDG.API;
using BarDoDG.Application.Constants;
using BarDoDG.Application.DTOs;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BarDoDG.Test.Controllers
{
    public class ComandaControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly string url = "v1/comandas";

        public ComandaControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();            
        }

        [Fact]
        public virtual async Task GetAllComandas()
        {
            #region Buscar todas comandas
            // faz a chamada na api
            var httpResponse = await _client.GetAsync($"{url}");
            // verifica se o retorno foi OK (200)
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            #endregion
        }

        [Fact]
        public virtual async Task InsertComandaWithoutCliente()
        {
            #region Inserir comanda sem cliente
            ComandaDTO comandaDTO = new ComandaDTO()
            {
                IdCliente = 0,
                NomeCliente = null
            };

            // faz a chamada na api
            var httpResponse = await _client.PostAsync($"{url}", new StringContent(JsonConvert.SerializeObject(comandaDTO), UnicodeEncoding.UTF8, "application/json"));            
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            APIBaseResponse apiBaseResponse = JsonConvert.DeserializeObject<APIBaseResponse>(stringResponse);
            Assert.Contains("Cliente não encontrado.", apiBaseResponse.Message);
            #endregion
        }

        [Fact]
        public virtual async Task InsertComandaWithCliente()
        {
            #region Inserir comanda com cliente
            ComandaInsertDTO comandaInsertDTO = new ComandaInsertDTO()
            {                
                NomeCliente = "Teste de API"
            };

            // faz a chamada na api
            var httpResponse = await _client.PostAsync($"{url}", new StringContent(JsonConvert.SerializeObject(comandaInsertDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();           
            int idComanda = JsonConvert.DeserializeObject<int>(stringResponse);
            Assert.True(idComanda > 0);
            #endregion
        }

        [Fact]
        public virtual async Task InsertComandaAndItem()
        {
            #region Inserir Comanda
            ComandaInsertDTO comandaInsertDTO = new ComandaInsertDTO()
            {
                NomeCliente = "Teste de API com Item"
            };

            // faz a chamada na api
            var httpResponse = await _client.PostAsync($"{url}", new StringContent(JsonConvert.SerializeObject(comandaInsertDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            int idComanda = JsonConvert.DeserializeObject<int>(stringResponse);
            Assert.True(idComanda > 0);
            #endregion

            #region Inserir item na comanda
            // faz a chamada na api de itemComprados
            string urlItemComprado = "v1/ItensComprados";

            ItemCompradoDTO itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = 0
            };
            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

            stringResponse = await httpResponse.Content.ReadAsStringAsync();
            APIBaseResponse apiBaseResponse = JsonConvert.DeserializeObject<APIBaseResponse>(stringResponse);
            Assert.Equal("Comanda e item são obrigatórios.", apiBaseResponse.Message);

            itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = Consts.Item.Cerveja
            };

            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            #endregion
        }

        [Fact]
        public virtual async Task InsertComandaAndItemAndValidatedTotalComanda()
        {
            #region Inserir Comanda
            ComandaInsertDTO comandaInsertDTO = new ComandaInsertDTO()
            {                
                NomeCliente = "Teste de API com Item"
            };

            // faz a chamada na api
            var httpResponse = await _client.PostAsync($"{url}", new StringContent(JsonConvert.SerializeObject(comandaInsertDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            int idComanda = JsonConvert.DeserializeObject<int>(stringResponse);
            Assert.True(idComanda > 0);
            #endregion

            #region Inserir item na comanda
            // faz a chamada na api de itemComprados
            string urlItemComprado = "v1/ItensComprados";

            ItemCompradoDTO itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = 0
            };
            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

            stringResponse = await httpResponse.Content.ReadAsStringAsync();
            APIBaseResponse apiBaseResponse = JsonConvert.DeserializeObject<APIBaseResponse>(stringResponse);
            Assert.Equal("Comanda e item são obrigatórios.", apiBaseResponse.Message);

            itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = Consts.Item.Conhaque
            };

            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            #endregion

            #region Validar total comanda
            httpResponse = await _client.GetAsync($"{url}/{idComanda}");
            // verifica se o retorno foi OK (200)
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

            stringResponse = await httpResponse.Content.ReadAsStringAsync();            
            ComandaDTO comandaDTO = JsonConvert.DeserializeObject<ComandaDTO>(stringResponse);
            Assert.Equal(20, comandaDTO.ValorTotal);
            #endregion
        }

        /// <summary>
        /// Inserir comanda, item, depois fechá-la e tentar adicionar mais item
        /// </summary>
        /// <returns></returns>
        [Fact]
        public virtual async Task InsertComandaAndItemAndCloseComanda()
        {
            #region Inserir Comanda
            ComandaInsertDTO comandaInsertDTO = new ComandaInsertDTO()
            {               
                NomeCliente = "Teste fechando comanda"
            };

            // faz a chamada na api
            var httpResponse = await _client.PostAsync($"{url}", new StringContent(JsonConvert.SerializeObject(comandaInsertDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            int idComanda = JsonConvert.DeserializeObject<int>(stringResponse);
            Assert.True(idComanda > 0);
            #endregion

            #region Inserir item na comanda
            // faz a chamada na api de itemComprados
            string urlItemComprado = "v1/ItensComprados";

            ItemCompradoDTO itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = Consts.Item.Suco
            };

            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

            itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = Consts.Item.Cerveja
            };

            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            #endregion

            #region Validar total comanda
            httpResponse = await _client.GetAsync($"{url}/{idComanda}");            
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

            stringResponse = await httpResponse.Content.ReadAsStringAsync();            
            ComandaDTO comandaDTO = JsonConvert.DeserializeObject<ComandaDTO>(stringResponse);
            Assert.Equal(55, comandaDTO.ValorTotal);
            Assert.Equal(2, comandaDTO.Desconto);
            Assert.Equal(53, comandaDTO.ValorTotalComDesconto);
            #endregion

            #region Encerrar comanda
            var httpResponsePatch = await _client.PatchAsync($"{url}/{idComanda}", null);
            Assert.Equal(HttpStatusCode.NoContent, httpResponsePatch.StatusCode);
            #endregion

            #region Tentar inserir novos itens na comanda fechada
            itemCompradoDTO = new ItemCompradoDTO()
            {
                IdComanda = idComanda,
                IdItem = Consts.Item.Cerveja
            };

            httpResponse = await _client.PostAsync($"{urlItemComprado}", new StringContent(JsonConvert.SerializeObject(itemCompradoDTO), UnicodeEncoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

            stringResponse = await httpResponse.Content.ReadAsStringAsync();
            APIBaseResponse apiBaseResponse = JsonConvert.DeserializeObject<APIBaseResponse>(stringResponse);
            Assert.Equal("Essa comanda já foi encerrada! Não é possível inserir mais itens.", apiBaseResponse.Message);
            #endregion
        }
    }
}
