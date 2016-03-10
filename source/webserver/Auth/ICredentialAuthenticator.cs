namespace webserver.Auth
{
    public interface ICredentialAuthenticator
    {
        AuthenticationResult Authenticate(LogInRequest login);
    }
}