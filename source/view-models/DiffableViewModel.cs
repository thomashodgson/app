using models;

namespace view_models
{
    public class DiffableViewModel
    {
        public Diff Diff { get; }
        public bool IsLoading { get; }

        public DiffableViewModel(Diff diff, bool isLoading)
        {
            Diff = diff;
            IsLoading = isLoading;
        }
    }
}
