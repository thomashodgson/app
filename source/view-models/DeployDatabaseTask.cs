using models;

namespace view_models
{
    public class DeployDatabaseTask
    {
        public Database Database { get; set; }
        public Database SourceDatabase { get; }

        public DeployDatabaseTask(Database database, Database sourceDatabase)
        {
            Database = database;
            SourceDatabase = sourceDatabase;
        }
    }
}