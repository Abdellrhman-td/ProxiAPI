using Microsoft.EntityFrameworkCore;
using ProxiWorkAPI.Data;
using ProxiWorkAPI.Dto;
using ProxiWorkAPI.Helpers;
using ProxiWorkAPI.Interfaces;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Respository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _context;

        public ApplicationRepository(DataContext context)
        {
            _context = context;
        }

        private static ApplicationDto MapToDto(Application a) => new ApplicationDto
        {
            Id = a.Id,
            JobId = a.JobId,
            JobTitle = a.Job?.Title ?? string.Empty,
            JobCity = a.Job?.City ?? string.Empty,
            JobSalary = a.Job?.Salary ?? 0,
            ApplicantId = a.ApplicantId,
            ApplicantName = a.Applicant?.FullName ?? string.Empty,
            ApplicantPhone = a.Applicant?.PhoneNumber,
            CoverMessage = a.CoverMessage,
            CvImageUrl = a.CvImageUrl,
            EmployerNote = a.EmployerNote,
            Status = (int)a.Status,
            DistanceKm = a.DistanceKm,
            AppliedAt = a.AppliedAt,
            ReviewedAt = a.ReviewedAt
        };

        public async Task<ApplicationDto> Apply(Application app)
        {
            var job = await _context.Jobs.FindAsync(app.JobId);
            var applicant = await _context.Users.FindAsync(app.ApplicantId);

            if (job != null && applicant != null)
            {
                app.DistanceKm = GeoHelper.CalculateDistance(
                    applicant.Latitude, applicant.Longitude,
                    job.Latitude, job.Longitude);
            }

            app.AppliedAt = DateTime.UtcNow;
            app.Status = ApplicationStatus.Pending;
            _context.Applications.Add(app);
            await _context.SaveChangesAsync();

            await _context.Entry(app).Reference(a => a.Job).LoadAsync();
            await _context.Entry(app).Reference(a => a.Applicant).LoadAsync();

            return MapToDto(app);
        }

        public async Task<List<ApplicationDto>> GetUserApplications(int applicantId)
        {
            var apps = await _context.Applications
                .Include(a => a.Job)
                    .ThenInclude(j => j.Employer)
                .Include(a => a.Applicant)
                .Where(a => a.ApplicantId == applicantId)
                .OrderByDescending(a => a.AppliedAt)
                .ToListAsync();

            return apps.Select(MapToDto).ToList();
        }

        public async Task<List<ApplicationDto>> GetJobApplications(int jobId)
        {
            var apps = await _context.Applications
                .Include(a => a.Applicant)
                .Include(a => a.Job)
                .Where(a => a.JobId == jobId)
                .OrderBy(a => a.DistanceKm)
                .ToListAsync();

            return apps.Select(MapToDto).ToList();
        }

        public async Task<ApplicationDto?> UpdateStatus(int id, ApplicationStatus status)
        {
            var app = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (app == null) return null;

            app.Status = status;
            app.ReviewedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return MapToDto(app);
        }

        public async Task<bool> HasApplied(int jobId, int applicantId)
        {
            return await _context.Applications
                .AnyAsync(a => a.JobId == jobId && a.ApplicantId == applicantId);
        }
    }
}