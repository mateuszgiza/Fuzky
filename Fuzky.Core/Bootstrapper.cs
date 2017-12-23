using Autofac;
using System;

namespace Fuzky.Core
{
    public static class Bootstrapper
    {
        public static IContainer Initialize()
        {
            //Autofac.ContainerBuilder
            var builder = new ContainerBuilder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyModules(assemblies);

            var container = builder.Build();
            //ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            return container;
        }
    }
}