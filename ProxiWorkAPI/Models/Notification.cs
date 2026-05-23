using System.ComponentModel.DataAnnotations;

namespace ProxiWorkAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Required, MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public string? Type { get; set; }
    }
}