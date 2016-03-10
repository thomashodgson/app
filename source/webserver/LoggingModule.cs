using System.Reflection;
using Autofac;
using Serilog;
using Serilog.Events;
using Module = Autofac.Module;

namespace webserver
{
    internal class LoggingModule : Module
    {
        private const LogEventLevel LogLevel = LogEventLevel.Debug;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(LoggerFactory());
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

        private static string GetEntryAssemblyName()
        {
            var entryAssembly = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
            var assemblyName = entryAssembly.GetName();
            return assemblyName.Name + "-" + assemblyName.Version;
        }
    }
}
