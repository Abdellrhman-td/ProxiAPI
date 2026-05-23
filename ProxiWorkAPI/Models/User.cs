using System.ComponentModel.DataAnnotations;

namespace ProxiWorkAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress, MaxLength(100)]
        public string? Email { get; set; }

        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public UserType UserType { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? City { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public double Rating { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastSeenAt { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Job> PostedJobs { get; set; } = new List<Job>();
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

    public enum UserType
    {
        JobSeeker = 1,
        Employer = 2,
        Admin = 3
    }
}