using System.Collections.Generic;

namespace view_models
{
    public class TasksGrid
    {
        public IEnumerable<CommitTask> CommitTasks { get; }
        public IEnumerable<DeployVersionTask> DeployVersionTasks { get; }
        public IEnumerable<DeployDatabaseTask> DeployDatabaseTasks { get; }
        public bool IsLoading { get; }

        public TasksGrid(IEnumerable<CommitTask> commitTasks, IEnumerable<DeployVersionTask> deployVersionTasks, IEnumerable<DeployDatabaseTask> deployDatabaseTasks, bool isLoading)
        {
            CommitTasks = commitTasks;
            DeployVersionTasks = deployVersionTasks;
            DeployDatabaseTasks = deployDatabaseTasks;
            IsLoading = isLoading;
        }
    }
}