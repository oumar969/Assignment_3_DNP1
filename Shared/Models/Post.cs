
using Shared.Models;

namespace Shared.Models;

public class Post
{
    public int Id { get; set; } // Unique identifier for the post
    public User Owner { get;  } // Foreign key to link the post to the user who created it
    public string Title { get; } // Title of the post
    public string Body { get;  } // Content of the post

    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
    
}
