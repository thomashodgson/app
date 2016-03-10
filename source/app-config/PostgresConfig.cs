namespace app_config
{
    public class PostgresConfig
    {
        public string Hostname { get; }
        public int Port { get;}
        public string Username { get; }
        public string Password { get; }
        public string DatabaseName { get; }

        public PostgresConfig(string hostname, int port, string username, string password, string databaseName)
        {
            Hostname = hostname;
            Port = port;
            Username = username;
            Password = password;
            DatabaseName = databaseName;
        }

        public string ToConnectionString()
        {
            return $"Host={Hostname};Username={Username};Password={Password};Database={DatabaseName};Port={Port}";
        }
    }
}