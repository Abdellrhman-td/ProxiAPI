namespace ProxiWorkAPI.Dto
{
    public class CreateApplicationDto
    {
        public int JobId { get; set; }
        public string? CoverMessage { get; set; }
        public string? CvImageUrl { get; set; }
    }
}