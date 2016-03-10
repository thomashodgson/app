using models;

namespace view_models
{
    public class DeployVersionTask
    {
        public Database Database { get; }
        public GitRepository Repository { get; }

        public DeployVersionTask(Database database, GitRepository repository)
        {
            Database = database;
            Repository = repository;
        }
    }
}