using ArcheryTraining.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ArcheryTraining.Interfaces
{
    public interface ISessionRepository : IBaseRepository
    {
        Task<IQueryable<Session>> GetAllUserSessionsAsync();
        Task<IQueryable<Session>> GetUserSessionsAsync(string userId);
        Task<Session> WriteSessionAsync(Session entity);
    }
}
