using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTrackingSystem.API.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected bool TryGetUserId(out Guid userId)
        {
            userId = Guid.Empty;
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdString, out userId);
        }
    }
}
