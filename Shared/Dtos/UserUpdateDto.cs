namespace Shared.Dtos;

public class UserUpdateDto
{
    public int Id { get; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    
    public UserUpdateDto(int id)
    {
        Id = id;
    }
}