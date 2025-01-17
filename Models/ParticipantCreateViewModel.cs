namespace EcfCdaDotNet.Models
{
    public class ParticipantCreateViewModel
    {
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public Guid EventId { get; set; }
    }
}