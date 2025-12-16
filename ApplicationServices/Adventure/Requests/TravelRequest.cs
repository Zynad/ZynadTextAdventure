namespace ApplicationServices.Adventure.Requests;

public class TravelRequest
{
    public Guid CharacterId { get; set; }

    public string DestinationId { get; set; } = string.Empty;
}
