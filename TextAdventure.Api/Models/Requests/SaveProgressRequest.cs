using System.ComponentModel.DataAnnotations;
using TextAdventure.Api.Models.State;

namespace TextAdventure.Api.Models.Requests;

public class SaveProgressRequest
{
    [Required]
    public string Token { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Level { get; set; }

    [Range(0, int.MaxValue)]
    public int Experience { get; set; }

    [Required]
    public AdventureState AdventureState { get; set; } = new();
}
