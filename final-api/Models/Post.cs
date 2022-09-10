using System.ComponentModel.DataAnnotations;

namespace final_api.Models;

public class Post
{
    public int PostId { get; set; }

    // Do I need to connect this to the user id as a foreign key? (maybe look more into this in the future) -- so that we can connect to the user object and get properties from it (https://docs.microsoft.com/en-us/ef/core/modeling/relationships)
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public string? Title { get; set; }

    [Required]
    public string? Content { get; set; }
    
    public string? PostedDate { get; set; }

}