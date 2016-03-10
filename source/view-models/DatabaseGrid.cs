using System.Collections.Generic;
using models;

namespace view_models
{
    public class DatabaseGrid
    {
        public IEnumerable<Database> Databases { get; }
        public bool IsLoading { get; }

        public DatabaseGrid(IEnumerable<Database> databases, bool isLoading = false)
        {
            Databases = databases;
            IsLoading = isLoading;
        }
    }
}
