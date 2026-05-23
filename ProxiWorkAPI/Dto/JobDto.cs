using System.ComponentModel.DataAnnotations;
namespace ProxiWorkAPI.Dto
{
    public class JobDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public decimal Salary { get; set; }
        public string WorkingHours { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int VacanciesCount { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
        public double RadiusKm { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int EmployerId { get; set; }
        public string EmployerName { get; set; } = string.Empty;
        public string? EmployerCity { get; set; }
        public string? EmployerImage { get; set; }
        public double EmployerRating { get; set; }
    }
}