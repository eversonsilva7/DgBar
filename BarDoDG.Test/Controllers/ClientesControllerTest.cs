using BarDoDG.API;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BarDoDG.Test.Controllers
{
    public class ClientesControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly string url = "v1/clientes";

        public ClientesControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJwZXNzIjoiVmhKbjlPaDIzdG89Iiwibm9tZSI6IlNkUjg5akhqSlhUd3NzSFVQRXRLdjh6cUtkU1EzZDY1R3JCYU8wVUpKUTV5cFJ4VjlhRmh3Rm00ckY1cjlEeGwiLCJjdXN0IjoiTWhWTituejFHZms9IiwiY29vcCI6IlZoSm45T2gyM3RvPSIsInNpc3QiOiJWaEpuOU9oMjN0bz0iLCJjYXJkIjoiMUJPWWtmNVhxZ1N1VXE4QXJPdm1FWW0wOHczQmVMYmMiLCJuYmYiOjE1OTU1MTU4MDMsImV4cCI6MTY5NTUxNTgwMiwiaWF0IjoxNTk1NTE1ODAzLCJpc3MiOiJodHRwOi8vd3d3LnNhdmVtYWlzLmNvbS5ici8iLCJhdWQiOiJodHRwOi8vd3d3LnNhdmVtYWlzLmNvbS5ici8ifQ.OaVnX42nWJRrvUbjJDRyiakbCihZhIx06jc9SqJIF-g");
        }

        [Fact]
        public virtual async Task GetAllClientes()
        {
            // faz a chamada na api
            var httpResponse = await _client.GetAsync($"{url}");

            // verifica se o retorno foi OK (200)
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
