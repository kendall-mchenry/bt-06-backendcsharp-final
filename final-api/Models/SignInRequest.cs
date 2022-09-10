using System.ComponentModel.DataAnnotations;

namespace final_api.Models;

public class SignInRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}