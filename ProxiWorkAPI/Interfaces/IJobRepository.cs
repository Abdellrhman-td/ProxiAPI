using ProxiWorkAPI.Dto;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Interfaces
{
    public interface IJobRepository
    {
        Task<List<JobDto>> GetAllJobs();
        Task<JobDto?> GetJobById(int id);
        Task<Job> CreateJob(Job job);
        Task<JobDto?> UpdateJob(int id, Job job);
        Task<bool> DeleteJob(int id);
        Task<List<NearbyJobDto>> GetNearbyJobs(double lat, double lng, double radiusKm);
        Task IncrementViewCount(int id);
    }
}