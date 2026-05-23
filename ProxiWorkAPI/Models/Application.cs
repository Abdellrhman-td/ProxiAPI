using System.ComponentModel.DataAnnotations;

namespace ProxiWorkAPI.Models
{
    public class Application
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }
        public virtual User Applicant { get; set; } = null!;

        public int JobId { get; set; }
        public virtual Job Job { get; set; } = null!;

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAt { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        [MaxLength(500)]
        public string? CvImageUrl { get; set; }

        [MaxLength(1000)]
        public string? CoverMessage { get; set; }

        public string? EmployerNote { get; set; }
        public double? DistanceKm { get; set; }
    }

    public enum ApplicationStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Withdrawn = 4
    }
}