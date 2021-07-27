using ArcheryTraining.Constants;
using ArcheryTraining.Interfaces;
using ArcheryTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcheryTraining.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IAuthenticationService authenticationService;

        public SecurityService()
        {
            this.authenticationService = DependencyService.Get<IAuthenticationService>();
        }

        public async Task<(bool success, string error)> AuthenticateProcessAsync()
        {
            var authResult = await authenticationService.Authenticate();

            if (authResult.IsError)
            {
                return (false, authResult.Error);
            }

            await StoreInformations(authResult);

            return (true, string.Empty);
        }

        public async Task<(bool success, string error)> RefreshTokenProcessAsync()
        {
            var authResult = await authenticationService.Authenticate();

            if (authResult.IsError)
            {
                return (false, authResult.Error);
            }

            await StoreInformations(authResult);

            return (true, string.Empty);
        }

        public async Task<string> GetTokenAsync()
        {
            var expiry = await SecureStorage.GetAsync(SettingsConstants.AccessTokenExpire);

            if (string.IsNullOrEmpty(expiry) || DateTime.Parse(expiry) < DateTime.UtcNow)
            {
                await RefreshTokenProcessAsync();
            }

            var token = await SecureStorage.GetAsync(SettingsConstants.AccessToken);

            return token;
        }

        private async Task StoreInformations(AuthenticationResult authenticationResult)
        {
            await SecureStorage.SetAsync(SettingsConstants.UserId, authenticationResult.UserClaims.FirstOrDefault(f => f.Type == "sub")?.Value);
            await SecureStorage.SetAsync(SettingsConstants.AccessToken, authenticationResult.AccessToken);
            await SecureStorage.SetAsync(SettingsConstants.RefreshToken, authenticationResult.RefreshToken);
            await SecureStorage.SetAsync(SettingsConstants.AccessTokenExpire, authenticationResult.ExpireAt.ToString());

            var claimsAsDict = new Dictionary<string, string>();
            
            foreach(var claim in authenticationResult.UserClaims)
            {
                claimsAsDict.Add(claim.Type, claim.Value);
            }

            await SecureStorage.SetAsync(SettingsConstants.UserClaims, JsonSerializer.Serialize(claimsAsDict));
        }
    }
}
