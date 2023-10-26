namespace Shared.Dtos;

public class PostCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }

    public string Body { get; }

    public PostCreationDto(int ownerId, string title, string body)
    {
        OwnerId = ownerId;
        Title = title;
        Body = body;
    }
    
    public override string ToString()
    {
        return "ownerId: " + OwnerId + "/title: " + Title + "/body: " + Body;
    }
}