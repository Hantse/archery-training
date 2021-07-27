using ArcheryTraining.Core;
using ArcheryTraining.Interfaces;
using Shared.Contracts.Response;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArcheryTraining.Services
{
    public class SessionService : BaseHttpService, ISessionService
    {
        public async Task<SessionHistoryItem[]> GetUserSessionsHistoryAsync()
        {
            var client = await HttpClientSecure();
            var response = await client.GetAsync(new Uri($"https://c634ef01768c.ngrok.io/Session"));
            return JsonSerializer.Deserialize<SessionHistoryItem[]>(await response.Content.ReadAsStringAsync());
        }
    }
}
