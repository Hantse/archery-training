using ArcheryTraining.Constants;
using ArcheryTraining.Interfaces;
using Polly;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcheryTraining.Core
{
    public class BaseHttpService
    {
        protected readonly AsyncRetryPolicy<HttpResponseMessage> httpRequestPolicy;
        private readonly ISecurityService securityService;
        public BaseHttpService()
        {
            securityService = DependencyService.Get<ISecurityService>();

            httpRequestPolicy = Policy.HandleResult<HttpResponseMessage>(
                                    r => r.StatusCode == HttpStatusCode.InternalServerError ||
                                         r.StatusCode == HttpStatusCode.Unauthorized)
                                        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                                            onRetry: async (response, timespan) =>
                                            {
                                                if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                                                {
                                                    _ = await securityService.RefreshTokenProcessAsync();
                                                }
                                            });
        }

        public HttpClient HttpClient()
        {
            var httpClient = new HttpClient();

            return httpClient;
        }

        public async Task<HttpClient> HttpClientSecure()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(SettingsConstants.AccessToken));
            return httpClient;
        }

        public HttpContent GetMessage(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
