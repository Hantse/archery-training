using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryTraining.Interfaces
{
    public interface ISecurityService
    {
        Task<(bool success, string error)> AuthenticateProcessAsync();
        Task<(bool success, string error)> RefreshTokenProcessAsync();
        Task<string> GetTokenAsync();
    }
}
