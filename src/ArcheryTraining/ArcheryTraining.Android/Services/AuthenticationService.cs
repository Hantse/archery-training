using ArcheryTraining.Configurations;
using ArcheryTraining.Droid.Services;
using ArcheryTraining.Interfaces;
using ArcheryTraining.Models;
using Auth0.OidcClient;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace ArcheryTraining.Droid.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private Auth0Client _auth0Client;

        public AuthenticationService()
        {
            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = AuthenticationConfig.Domain,
                ClientId = AuthenticationConfig.ClientId,
                Scope = "openid offline_access profile"
            });
        }

        public AuthenticationResult AuthenticationResult { get; private set; }

        public async Task<AuthenticationResult> Authenticate()
        {
            var auth0LoginResult = await _auth0Client.LoginAsync(new { audience = AuthenticationConfig.Audience });
            AuthenticationResult authenticationResult;

            if (!auth0LoginResult.IsError)
            {
                authenticationResult = new AuthenticationResult()
                {
                    AccessToken = auth0LoginResult.AccessToken,
                    IdToken = auth0LoginResult.IdentityToken,
                    UserClaims = auth0LoginResult.User.Claims,
                    RefreshToken = auth0LoginResult.RefreshToken,
                    ExpireAt = auth0LoginResult.AccessTokenExpiration.UtcDateTime
                };
            }
            else
                authenticationResult = new AuthenticationResult(auth0LoginResult.IsError, auth0LoginResult.Error);

            AuthenticationResult = authenticationResult;
            return authenticationResult;
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token)
        {
            var auth0LoginResult = await _auth0Client.RefreshTokenAsync(token);
            AuthenticationResult authenticationResult;

            if (!auth0LoginResult.IsError)
            {
                authenticationResult = new AuthenticationResult()
                {
                    AccessToken = auth0LoginResult.AccessToken,
                    IdToken = auth0LoginResult.IdentityToken,
                    RefreshToken = auth0LoginResult.RefreshToken,
                    ExpireAt = auth0LoginResult.AccessTokenExpiration.UtcDateTime
                };
            }
            else
                authenticationResult = new AuthenticationResult(auth0LoginResult.IsError, auth0LoginResult.Error);

            AuthenticationResult = authenticationResult;
            return authenticationResult;
        }
    }
}
