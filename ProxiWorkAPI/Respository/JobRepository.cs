using Microsoft.EntityFrameworkCore;
using ProxiWorkAPI.Data;
using ProxiWorkAPI.Dto;
using ProxiWorkAPI.Helpers;
using ProxiWorkAPI.Interfaces;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Respository
{
    public class JobRepository : IJobRepository
    {
        private readonly DataContext _context;

        public JobRepository(DataContext context)
        {
            _context = context;
        }

        private static JobDto MapToDto(Job j) => new JobDto
        {
            Id = j.Id,
            Title = j.Title,
            Description = j.Description,
            Salary = j.Salary,
            WorkingHours = j.WorkingHours,
            RequiredSkills = j.RequiredSkills,
            City = j.City,
            Latitude = j.Latitude,
            Longitude = j.Longitude,
            VacanciesCount = j.VacanciesCount,
            IsFeatured = j.IsFeatured,
            ViewCount = j.ViewCount,
            RadiusKm = j.RadiusKm,
            CreatedAt = j.CreatedAt,
            ExpiresAt = j.ExpiresAt,
            EmployerId = j.EmployerId,
            EmployerName = j.Employer?.FullName ?? string.Empty,
            EmployerCity = j.Employer?.City,
            EmployerImage = j.Employer?.ProfileImageUrl,
            EmployerRating = j.Employer?.Rating ?? 0
        };

        public async Task<List<JobDto>> GetAllJobs()
        {
            var jobs = await _context.Jobs
                .Include(j => j.Employer)
                .Where(j => j.IsActive)
                .OrderByDescending(j => j.IsFeatured)
                .ThenByDescending(j => j.CreatedAt)
                .ToListAsync();

            return jobs.Select(MapToDto).ToList();
        }

        public async Task<JobDto?> GetJobById(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Employer)
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(j => j.Id == id);

            return job == null ? null : MapToDto(job);
        }

        public async Task<Job> CreateJob(Job job)
        {
            job.CreatedAt = DateTime.UtcNow;
            job.IsActive = true;
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<JobDto?> UpdateJob(int id, Job updatedJob)
        {
            var job = await _context.Jobs
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null) return null;

            job.Title = updatedJob.Title;
            job.Description = updatedJob.Description;
            job.Salary = updatedJob.Salary;
            job.WorkingHours = updatedJob.WorkingHours;
            job.RequiredSkills = updatedJob.RequiredSkills;
            job.City = updatedJob.City;
            job.Latitude = updatedJob.Latitude;
            job.Longitude = updatedJob.Longitude;
            job.RadiusKm = updatedJob.RadiusKm;
            job.VacanciesCount = updatedJob.VacanciesCount;
            job.IsActive = updatedJob.IsActive;
            job.ExpiresAt = updatedJob.ExpiresAt;

            await _context.SaveChangesAsync();
            return MapToDto(job);
        }

        public async Task<bool> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NearbyJobDto>> GetNearbyJobs(
            double lat, double lng, double radiusKm = 10)
        {
            var jobs = await _context.Jobs
                .Include(j => j.Employer)
                .Where(j => j.IsActive && j.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();

            return jobs
                .Select(j =>
                {
                    var distance = GeoHelper.CalculateDistance(lat, lng, j.Latitude, j.Longitude);
                    return new NearbyJobDto
                    {
                        Id = j.Id,
                        Title = j.Title,
                        Description = j.Description,
                        Salary = j.Salary,
                        WorkingHours = j.WorkingHours,
                        RequiredSkills = j.RequiredSkills,
                        City = j.City,
                        Latitude = j.Latitude,
                        Longitude = j.Longitude,
                        VacanciesCount = j.VacanciesCount,
                        IsFeatured = j.IsFeatured,
                        EmployerName = j.Employer?.FullName ?? string.Empty,
                        Distance = distance,
                        DistanceText = GeoHelper.FormatDistance(distance)
                    };
                })
                .Where(j => j.Distance <= radiusKm)
                .OrderByDescending(j => j.IsFeatured)
                .ThenBy(j => j.Distance)
                .ToList();
        }

        public async Task IncrementViewCount(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return;
            job.ViewCount++;
            await _context.SaveChangesAsync();
        }
    }
}