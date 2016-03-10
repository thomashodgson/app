using System;

namespace webserver.Auth
{
    internal class AuthoriseAutomatically : ICredentialAuthenticator
    {
        public AuthenticationResult Authenticate(LogInRequest login)
        {
            return new AuthenticationResult(true, Guid.NewGuid().ToString());
        }
    }
}
