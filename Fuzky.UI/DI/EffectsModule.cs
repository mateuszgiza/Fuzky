using Autofac;
using Fuzky.Core;
using System.Windows.Media.Effects;

namespace Fuzky.UI.DI
{
    public class EffectsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new BlurEffect { Radius = 5 })
                .Keyed<object>(DiKeys.Effects.Blur);
        }
    }
}