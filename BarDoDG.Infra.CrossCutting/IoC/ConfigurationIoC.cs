using Autofac;
using BarDoDG.Application;
using BarDoDG.Application.Interfaces;
using BarDoDG.Application.Interfaces.Mappers;
using BarDoDG.Application.Mappers;
using BarDoDG.Domain.Interfaces.Repositories;
using BarDoDG.Domain.Interfaces.Services;
using BarDoDG.Domain.Services;
using BarDoDG.Infra.Data.Repositories;

namespace BarDoDG.Infra.CrossCutting.IoC
{
    public class ConfigurationIoC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IoC
            builder.RegisterType<ApplicationServiceCliente>().As<IApplicationServiceCliente>();
            builder.RegisterType<ApplicationServiceItem>().As<IApplicationServiceItem>();
            builder.RegisterType<ApplicationServiceItemComprado>().As<IApplicationServiceItemComprado>();
            builder.RegisterType<ApplicationServiceComanda>().As<IApplicationServiceComanda>();

            builder.RegisterType<ServiceCliente>().As<IServiceCliente>();
            builder.RegisterType<ServiceItem>().As<IServiceItem>();
            builder.RegisterType<ServiceItemComprado>().As<IServiceItemComprado>();
            builder.RegisterType<ServiceComanda>().As<IServiceComanda>();

            builder.RegisterType<RepositoryCliente>().As<IRepositoryCliente>();
            builder.RegisterType<RepositoryItem>().As<IRepositoryItem>();
            builder.RegisterType<RepositoryItemComprado>().As<IRepositoryItemComprado>();
            builder.RegisterType<RepositoryComanda>().As<IRepositoryComanda>();

            builder.RegisterType<MapperCliente>().As<IMapperCliente>();
            builder.RegisterType<MapperItem>().As<IMapperItem>();
            builder.RegisterType<MapperItemComprado>().As<IMapperItemComprado>();
            builder.RegisterType<MapperComanda>().As<IMapperComanda>();
            #endregion
        }
    }
}
