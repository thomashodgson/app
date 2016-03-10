using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using models.Urls;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using webserver.Auth;

namespace webserver.Controllers
{
    public class AccountController : ApiController
    {
        private readonly ICredentialAuthenticator _authenticator;

        public AccountController(ICredentialAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpPost, Route(RequestUrls.LogIn)]
        public IHttpActionResult LogIn(LogInRequest login)
        {
            // allready authenticated
            if (GetAuthentication().User != null)
            {
                return Ok();
            }

            var authenticationResult = _authenticator.Authenticate(login);
            if (authenticationResult.IsValid)
            {
                IdentitySignIn(authenticationResult.UserId);
                return Ok();
            }

            return StatusCode(HttpStatusCode.Forbidden);
        }

        [HttpPost, Route(RequestUrls.LogOut)]
        public void LogOut()
        {
            GetAuthentication()
                .SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
        }

        private void IdentitySignIn(string userId, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userId)
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var authenticationManager = GetAuthentication();

            authenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        private IAuthenticationManager GetAuthentication()
        {
            return Request.GetOwinContext().Authentication;
        }
    }
}
