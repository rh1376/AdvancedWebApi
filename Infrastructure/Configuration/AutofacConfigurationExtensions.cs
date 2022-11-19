using Autofac;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using Common;

namespace CleanArchitecture.Infrastructure.Configuration;

public static class AutofacConfigurationExtensions
{
    public static void AddServices(this ContainerBuilder containerBuilder)
    {
        //RegisterType > As > Liftetime
        containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        var entitiesAssembly = typeof(IEntity).Assembly;
        var dataAssembly = typeof(ApplicationDbContext).Assembly;

        containerBuilder.RegisterAssemblyTypes(entitiesAssembly, dataAssembly)
            .AssignableTo<IScopedDependency>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(entitiesAssembly, dataAssembly)
            .AssignableTo<ITransientDependency>()
            .AsImplementedInterfaces()
            .InstancePerDependency();

        containerBuilder.RegisterAssemblyTypes(entitiesAssembly, dataAssembly)
            .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}
