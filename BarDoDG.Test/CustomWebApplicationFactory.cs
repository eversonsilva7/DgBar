using BarDoDG.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BarDoDG.Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {        
    }
}
