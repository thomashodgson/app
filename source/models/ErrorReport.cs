namespace models
{
    public class ErrorReport
    {
        public string ErrorMessage { get; }
        public long Time { get; }

        public ErrorReport(string errorMessage, long time)
        {
            ErrorMessage = errorMessage;
            Time = time;
        }
    }
}