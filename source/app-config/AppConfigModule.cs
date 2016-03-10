using Autofac;

namespace app_config
{
    public class AppConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AppConfigProvider>().As<IConfigProvider>().SingleInstance();
        }
    }
}
