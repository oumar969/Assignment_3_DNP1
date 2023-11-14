
using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace Shared.Models;

public class Post
{
    [Key]
    public int Id { get; set; } // Unique identifier for the post
    public User Owner { get;  set; } // Foreign key to link the post to the user who created it
    public int OwnerId { get; set; }

    public string Title { get;  set; } // Title of the post
    public string Body { get; set; } // Content of the post

    public Post(int ownerId, string title, string body)
    {
        OwnerId = ownerId;
        Title = title;
        Body= body;
    }
    
    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
    private Post()
    {
        // Required by EF Core
    }
    
}
