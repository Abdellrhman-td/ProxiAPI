namespace ProxiWorkAPI.Dto
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string JobCity { get; set; } = string.Empty;
        public decimal JobSalary { get; set; }
        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public string? ApplicantPhone { get; set; }
        public string? CoverMessage { get; set; }
        public string? CvImageUrl { get; set; }
        public string? EmployerNote { get; set; }
        public int Status { get; set; }
        public double? DistanceKm { get; set; }
        public DateTime AppliedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }
}