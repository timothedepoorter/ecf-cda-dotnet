namespace EcfCdaDotNet.Models;

public partial class Event
{
    public Guid Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? Location { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Participation> Participations { get; set; } = new List<Participation>();
}
