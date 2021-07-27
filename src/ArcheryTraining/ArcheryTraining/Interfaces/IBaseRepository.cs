using Realms;
using System.Threading.Tasks;

namespace ArcheryTraining.Interfaces
{
    public interface IBaseRepository
    {
        Task<Realm> GetRealmAsync();
        Task RefreshRealmAsync();
    }
}
