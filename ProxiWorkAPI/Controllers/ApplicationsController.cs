using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProxiWorkAPI.Dto;
using ProxiWorkAPI.Interfaces;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationRepository _apps;

        public ApplicationsController(IApplicationRepository apps)
        {
            _apps = apps;
        }

        private int CurrentUserId
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(c =>
                    c.Type.Contains("nameidentifier") || c.Type == "sub");
                return int.Parse(claim!.Value);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Apply([FromBody] CreateApplicationDto dto)
        {
            if (await _apps.HasApplied(dto.JobId, CurrentUserId))
                return Conflict(new { message = "قدمت على الوظيفة دي قبل كده" });

            var app = new Application
            {
                JobId = dto.JobId,
                ApplicantId = CurrentUserId,
                CoverMessage = dto.CoverMessage,
                CvImageUrl = dto.CvImageUrl
            };

            var result = await _apps.Apply(app);
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyApplications()
        {
            var apps = await _apps.GetUserApplications(CurrentUserId);
            return Ok(apps);
        }

        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetJobApplications(int jobId)
        {
            var apps = await _apps.GetJobApplications(jobId);
            return Ok(apps);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id, [FromBody] UpdateStatusDto dto)
        {
            var app = await _apps.UpdateStatus(id, dto.Status);
            if (app == null)
                return NotFound(new { message = "الطلب مش موجود" });

            return Ok(app);
        }
    }

    public record UpdateStatusDto(ApplicationStatus Status);
}