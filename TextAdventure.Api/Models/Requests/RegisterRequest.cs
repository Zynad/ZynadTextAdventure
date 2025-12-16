using System.ComponentModel.DataAnnotations;

namespace TextAdventure.Api.Models.Requests;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
