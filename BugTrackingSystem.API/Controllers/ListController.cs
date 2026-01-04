using BugTrackingSystem.Application.Lists.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.API.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListController(IMediator mediator) : ControllerBase
    {
        [HttpGet("roles")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await mediator.Send(new GetRolesQuery());
            return Ok(roles);
        }

        [HttpGet("severity-levels")]
        [Authorize]
        public async Task<IActionResult> GetSeverityLevels()
        {
            var severityLevels = await mediator.Send(new GetSeverityLevelsQuery());
            return Ok(severityLevels);
        }

        [HttpGet("developers")]
        [Authorize]
        public async Task<IActionResult> GetDevelopers()
        {
            var developers = await mediator.Send(new GetDevelopersQuery());
            return Ok(developers);
        }

        [HttpGet("statuses")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBugStatuses()
        {
            var statuses = await mediator.Send(new GetBugStatusesQuery());
            return Ok(statuses);
        }
    }
}
