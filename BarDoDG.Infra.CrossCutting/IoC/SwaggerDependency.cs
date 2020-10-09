using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace BarDoDG.Infra.CrossCutting.IoC
{
    public static class SwaggerDependency
    {
        public static void AddSwaggerDependency(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Bar do DG API",
                        Version = "v1",
                        Description = "API REST created on ASP.NET Core 3.1",
                        Contact = new OpenApiContact
                        {
                            Name = "Everson Silva",
                            Url = new Uri("https://www.linkedin.com/in/eversoncssilva/"),
                        }
                    });
            });
        }

        public static void UseSwaggerDependency(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bar do DG API");
                c.DocumentTitle = "Bar do DG API";
                c.DocExpansion(DocExpansion.List);
            });
        }
    }
}
