
using Shared.Dtos;
using Shared.Models;

namespace HttpClients.ClientInterface;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        int? userId,
        string? titleContains
    );
    Task<PostBasicDto> GetByIdAsync(int id);
    Task UpdateAsync(PostUpdateDto dto);
    Task DeleteAsync(int id);
}