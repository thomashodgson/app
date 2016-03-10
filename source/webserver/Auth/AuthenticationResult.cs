namespace webserver.Auth
{
    public class AuthenticationResult
    {
        public bool IsValid { get; }
        public string UserId { get; }

        public AuthenticationResult(bool isValid, string userId)
        {
            IsValid = isValid;
            UserId = userId;
        }
    }
}