
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace final_api.Models;

public class User 
{
   // [JsonIgnore]
    public int UserId { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? State { get; set; }

    // OPTIONAL -- want to see if I can connect this to the individual posts (later)
    public string? PhotoUrl { get; set; }

    public string? CreatedDate { get; set; }
    
    [JsonIgnore]
    public List<Post>? Posts { get; set; }


}