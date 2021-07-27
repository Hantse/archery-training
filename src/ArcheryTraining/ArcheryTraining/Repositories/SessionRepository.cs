using ArcheryTraining.Constants;
using ArcheryTraining.Interfaces;
using ArcheryTraining.Models;
using Realms;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ArcheryTraining.Repositories
{
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        public SessionRepository() : base()
        {
        }

        public async Task<IQueryable<Session>> GetAllUserSessionsAsync()
        {
            var userId = await SecureStorage.GetAsync(SettingsConstants.UserId);
            var realm = await GetRealmAsync();

            var guestSessions = realm.All<Session>()
                .Filter($"Owner = '{userId}' OR Guests CONTAINS[c] '{userId}'");

            return guestSessions;
        }

        public async Task<IQueryable<Session>> GetUserSessionsAsync(string userId)
        {
            var realm = await GetRealmAsync();
            return realm.All<Session>()
                            .Where(s => s.Owner == userId);
        }

        public async Task<Session> WriteSessionAsync(Session entity)
        {
            var userId = await SecureStorage.GetAsync(SettingsConstants.UserId);
            var realm = await GetRealmAsync();
            entity.Owner = userId;
            
            realm.Write(() =>
            {
                realm.Add(entity);
            });

            return entity;
        }

        public async Task<IQueryable<Session>> GetChangeState()
        {
            var realm = await GetRealmAsync();
            return realm.All<Session>();
        }
    }
}
