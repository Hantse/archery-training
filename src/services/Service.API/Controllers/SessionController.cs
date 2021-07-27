using Infrastructure.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Service.API.Mocks;
using System.Threading.Tasks;

namespace Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : CoreController
    {
        [HttpGet]
        public async Task<IActionResult> GetUserSessionHistoryAsync()
        {
            return Ok(SessionMocks.GetUserSessionHistoryMocks());
        }
    }
}
