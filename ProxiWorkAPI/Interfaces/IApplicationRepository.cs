using ProxiWorkAPI.Dto;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Interfaces
{
    public interface IApplicationRepository
    {
        Task<ApplicationDto> Apply(Application app);
        Task<List<ApplicationDto>> GetUserApplications(int applicantId);
        Task<List<ApplicationDto>> GetJobApplications(int jobId);
        Task<ApplicationDto?> UpdateStatus(int id, ApplicationStatus status);
        Task<bool> HasApplied(int jobId, int applicantId);
    }
}