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
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobs;

        public JobsController(IJobRepository jobs)
        {
            _jobs = jobs;
        }

        private int CurrentUserId =>
            int.Parse(User.Claims.First(c => c.Type == "sub").Value);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _jobs.GetAllJobs();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobs.GetJobById(id);
            if (job == null)
                return NotFound(new { message = "الوظيفة مش موجودة" });

            await _jobs.IncrementViewCount(id);
            return Ok(job);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearby(
            [FromQuery] double lat,
            [FromQuery] double lng,
            [FromQuery] double radius = 10)
        {
            var jobs = await _jobs.GetNearbyJobs(lat, lng, radius);
            return Ok(jobs);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateJobDto dto)
        {
            var userType = User.Claims.FirstOrDefault(c => c.Type == "userType")?.Value;
            if (userType != "Employer")
                return Forbid();

            var employerId = CurrentUserId;

            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                Salary = dto.Salary,
                WorkingHours = dto.WorkingHours,
                RequiredSkills = dto.RequiredSkills,
                City = dto.City,
                VacanciesCount = dto.VacanciesCount,
                RadiusKm = dto.RadiusKm,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                ExpiresAt = dto.ExpiresAt,
                EmployerId = employerId
            };

            var created = await _jobs.CreateJob(job);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CreateJobDto dto)
        {
            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                Salary = dto.Salary,
                WorkingHours = dto.WorkingHours,
                RequiredSkills = dto.RequiredSkills,
                City = dto.City,
                VacanciesCount = dto.VacanciesCount,
                RadiusKm = dto.RadiusKm,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                ExpiresAt = dto.ExpiresAt,
                IsActive = true
            };

            var updated = await _jobs.UpdateJob(id, job);
            if (updated == null)
                return NotFound(new { message = "الوظيفة مش موجودة" });

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _jobs.DeleteJob(id);
            if (!deleted)
                return NotFound(new { message = "الوظيفة مش موجودة" });

            return Ok(new { message = "تم الحذف" });
        }
    }
}