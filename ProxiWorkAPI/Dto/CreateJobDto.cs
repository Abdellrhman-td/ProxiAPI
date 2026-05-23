using System.ComponentModel.DataAnnotations;

namespace ProxiWorkAPI.Dto
{
    public class CreateJobDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public decimal Salary { get; set; }
        public string WorkingHours { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int VacanciesCount { get; set; } = 1;
        public double RadiusKm { get; set; } = 5;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}