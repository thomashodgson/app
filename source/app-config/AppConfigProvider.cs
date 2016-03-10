using System;
using System.IO;
using Newtonsoft.Json;

namespace app_config
{
    public class AppConfigProvider : IConfigProvider
    {
        public Config GetConfig()
        {
            var configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));
            var withDocker = LookupFromDockerEnvironmentVariables(config);

            return withDocker;
        }

        private Config LookupFromDockerEnvironmentVariables(Config config)
        {
            // will exist if has been started with 'docker run --link app_rabbit'
            var isInsideLinkedDockerContainer = Environment.GetEnvironmentVariable("APP_RABBIT_NAME") != null;
            var postgresHost = "APP_POSTGRES_PORT_5432_TCP_ADDR";
            var rabbitPortTcpAddr = "APP_RABBIT_PORT_5672_TCP_ADDR";
            var rabbitMqHostname = Environment.GetEnvironmentVariable(rabbitPortTcpAddr);

            var postgreshost = Environment.GetEnvironmentVariable(postgresHost);

            return isInsideLinkedDockerContainer
                ? new Config(
                    new PostgresConfig(
                        postgreshost,
                        config.PostgresConfig.Port,
                        config.PostgresConfig.Username, config.PostgresConfig.Password,
                        config.PostgresConfig.DatabaseName),
                    rabbitMqHostname)
                : config;
        }
        
    }
}