using Shared.Models;
using Shared.Models;

namespace FileData;

public class DataContainer
// denne her klasse indeholder en liste af brugere og en liste af posts
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }
}