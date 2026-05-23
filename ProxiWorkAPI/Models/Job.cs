using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProxiWorkAPI.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }

        [MaxLength(200)]
        public string WorkingHours { get; set; } = string.Empty;

        public string RequiredSkills { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int VacanciesCount { get; set; } = 1;
        public double RadiusKm { get; set; } = 5;
        public bool IsFeatured { get; set; } = false;
        public int ViewCount { get; set; } = 0;

        // GPS
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int EmployerId { get; set; }
        public virtual User Employer { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}