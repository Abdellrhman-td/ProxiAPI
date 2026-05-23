namespace ProxiWorkAPI.Dto
{
    public class NearbyJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string WorkingHours { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int VacanciesCount { get; set; }
        public bool IsFeatured { get; set; }
        public string EmployerName { get; set; } = string.Empty;
        public double Distance { get; set; }
        public string DistanceText { get; set; } = string.Empty;
    }
}