using BugTrackingSystem.API.DTOs;
using BugTrackingSystem.Application.Bugs.Commands;
using BugTrackingSystem.Application.Bugs.DTOs;
using BugTrackingSystem.Application.Bugs.Queries;
using BugTrackingSystem.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.API.Controllers
{
    [Route("api/bug")]
    [ApiController]
    [Authorize]
    public class BugsController(IMediator mediator) : BaseApiController
    {
        [HttpPost("create-bug")]
        public async Task<IActionResult> CreateBug([FromForm] CreateBugFormRequest formRequest)
        {
            if (!TryGetUserId(out var userId)) return Unauthorized();

            var attachments = new List<FileAttachmentDto>();
            if (formRequest.Attachments != null)
            {
                foreach (var file in formRequest.Attachments)
                {
                    attachments.Add(new FileAttachmentDto
                    {
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        Length = file.Length,
                        Content = file.OpenReadStream()
                    });
                }
            }

            var request = new CreateBugRequest
            {
                Title = formRequest.Title,
                Description = formRequest.Description,
                Severity = formRequest.Severity,
                ReproductionSteps = formRequest.ReproductionSteps,
                AssignedToId = formRequest.AssignedToId,
                Attachments = attachments
            };

            var bugId = await mediator.Send(new CreateBugCommand(request, userId));
            return Ok(new { BugId = bugId });
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetBugById(Guid id)
        {
            var bug = await mediator.Send(new GetBugByIdQuery(id));
            if (bug == null) return NotFound();
            return Ok(bug);
        }

        [HttpPut("{id}/assign")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> AssignBug(Guid id, [FromBody] AssignBugRequest request)
        {
            if (!TryGetUserId(out var userId)) return Unauthorized();

            var targetDeveloperId = userId;
            
            await mediator.Send(new AssignBugCommand(id, targetDeveloperId));
            return Ok();
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateBugStatusRequest request)
        {
            await mediator.Send(new UpdateBugStatusCommand(id, request));
            return Ok();
        }

        [HttpGet("search")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> SearchBugs([FromQuery] SearchBugsRequest request)
        {
            var bugs = await mediator.Send(new SearchBugsQuery(request));
            return Ok(bugs);
        }

        [HttpGet("list-bugs")]
        public async Task<IActionResult> GetBugsList()
        {
            if (!TryGetUserId(out var userId)) return Unauthorized();

            var bugs = await mediator.Send(new GetUserBugsQuery(userId));
            return Ok(bugs);
        }
    }
}

