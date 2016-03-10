using System.Reflection;
using app_config;
using Autofac;
using Serilog;
using Serilog.Events;
using Module = Autofac.Module;

namespace app_base_module
{
    public class BaseModule : Module
    {
        public static LogEventLevel LogLevel = LogEventLevel.Debug;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(LoggerFactory());
            builder.RegisterModule<AppConfigModule>();
        }

        private static string GetEntryAssemblyName()
        {
            var entryAssembly = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
            var assemblyName = entryAssembly.GetName();
            return assemblyName.Name + "-" + assemblyName.Version;
        }

        private static ILogger LoggerFactory()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("AssemblyName", GetEntryAssemblyName())
                .MinimumLevel.Is(LogLevel)
                .WriteTo.ColoredConsole()
                .CreateLogger();
        }

    }
}