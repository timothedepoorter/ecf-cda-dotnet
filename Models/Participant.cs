namespace EcfCdaDotNet.Models;

public partial class Participant
{
    public Guid Id { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<Participation> Participations { get; set; } = new List<Participation>();
}
