namespace view_models
{
    public class Deployment
    {
        public long Utc { get; }
        public bool HasDriftedSinceDeployment { get; }
        public string RepositoryName { get; }
        public string Sha { get; }

        public Deployment(long utc, bool hasDriftedSinceDeployment, string repositoryName, string sha)
        {
            Utc = utc;
            HasDriftedSinceDeployment = hasDriftedSinceDeployment;
            RepositoryName = repositoryName;
            Sha = sha;
        }
    }
}