namespace EcfCdaDotNet.Models;

public partial class Participation
{
    public Guid IdParticipant { get; set; }

    public Guid IdEvent { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual Event IdEventNavigation { get; set; } = null!;

    public virtual Participant IdParticipantNavigation { get; set; } = null!;
}
