namespace EcfCdaDotNet.Models
{
    public class EventCreateViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
