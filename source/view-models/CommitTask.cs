using models;

namespace view_models
{
    public class CommitTask
    {
        public Database Database { get; }
        public GitRepository Repository { get; }

        public CommitTask(Database database, GitRepository repository)
        {
            Database = database;
            Repository = repository;
        }
    }
}