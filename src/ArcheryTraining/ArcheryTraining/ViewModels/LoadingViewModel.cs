using ArcheryTraining.Constants;
using ArcheryTraining.Interfaces;
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcheryTraining.ViewModels
{
    public class LoadingViewModel
    {
        private readonly ISecurityService securityService;

        public LoadingViewModel()
        {
            this.securityService = DependencyService.Get<ISecurityService>();
        }

        public async Task CheckAuthenticate()
        {
            var route = "//login";

            try
            {
                var oauthToken = await SecureStorage.GetAsync(SettingsConstants.AccessToken);
                var oauthTokenExpire = await SecureStorage.GetAsync(SettingsConstants.AccessTokenExpire);

                if (!string.IsNullOrEmpty(oauthToken)
                    && !string.IsNullOrEmpty(oauthTokenExpire))
                {
                    if (DateTime.TryParse(oauthTokenExpire, out DateTime expire))
                    {
                        if (expire < DateTime.UtcNow)
                        {
                            var refreshResult = await securityService.RefreshTokenProcessAsync();

                            if (!refreshResult.success)
                            {
                                route = "//login";
                            }
                        }
                    }

                    route = "//home";
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }

            await Shell.Current.GoToAsync(route);
        }
    }
}
