using ArcheryTraining.Constants;
using ArcheryTraining.Interfaces;
using Realms;
using Realms.Sync;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcheryTraining.Repositories
{
    public class BaseRepository
    {
        protected static Realms.Sync.App currentAppContext;
        protected static Realm currentRealm;
        private readonly ISecurityService securityService;

        public BaseRepository()
        {
            currentAppContext = Realms.Sync.App.Create(SettingsConstants.AppId);
            securityService = DependencyService.Get<ISecurityService>();
        }

        public async Task<Realm> GetRealmAsync()
        {
            if (currentRealm == null)
            {
                var userId = await SecureStorage.GetAsync(SettingsConstants.UserId);
                var user = await currentAppContext.LogInAsync(Credentials.JWT(await securityService.GetTokenAsync()));
                var config = new SyncConfiguration(userId, user);
                currentRealm = await Realm.GetInstanceAsync(config);
            }

            return currentRealm;
        }

        public async Task RefreshRealmAsync()
        {
            if (currentRealm == null)
            {
                var userId = await SecureStorage.GetAsync(SettingsConstants.UserId);
                var user = await currentAppContext.LogInAsync(Credentials.JWT(await securityService.GetTokenAsync()));
                var config = new SyncConfiguration(userId, user);
                currentRealm = await Realm.GetInstanceAsync(config);
            }
        }
    }
}
