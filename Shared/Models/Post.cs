namespace Shared.Auth;

public class Post
{
    public int PostId { get; set; } // Unique identifier for the post
    public string Title { get; set; } // Title of the post
    public string Body { get; set; } // Content of the post
    public User Owner { get; set; } // Foreign key to link the post to the user who created it

    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
    
}
