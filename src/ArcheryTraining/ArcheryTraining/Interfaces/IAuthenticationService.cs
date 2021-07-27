using ArcheryTraining.Models;
using System.Threading.Tasks;

namespace ArcheryTraining.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> Authenticate();
        Task<AuthenticationResult> RefreshTokenAsync(string token);
        AuthenticationResult AuthenticationResult { get; }
    }
}
