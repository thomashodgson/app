namespace app_config
{
    public class Config
    {
        public PostgresConfig PostgresConfig { get; }

        public string RabbitMqHostname { get; }

        public Config(PostgresConfig postgresConfig, string rabbitMqHostname)
        {
            PostgresConfig = postgresConfig;
            RabbitMqHostname = rabbitMqHostname;
        }
    }
}
