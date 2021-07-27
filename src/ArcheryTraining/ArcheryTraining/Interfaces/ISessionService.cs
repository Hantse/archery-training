using Shared.Contracts.Response;
using System.Threading.Tasks;

namespace ArcheryTraining.Interfaces
{
    public interface ISessionService
    {
        Task<SessionHistoryItem[]> GetUserSessionsHistoryAsync();
    }
}
