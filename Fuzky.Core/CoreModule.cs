using Autofac;

namespace Fuzky.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SteamAuthentication>()
                .AsSelf();
        }
    }
}