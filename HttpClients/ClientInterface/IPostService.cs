using Shared.Auth;
using Shared.Dtos;

namespace HttpClients.ClientInterface;


    public interface IPostService
    {
        Task CreateAsync(PostCreationDto dto);
        Task<ICollection<Post>> GetAsync(
            string? userName, 
            int? userId,
            string? titleContains
            //,string? bodyContains
        );
        
        
        Task<PostBasicDto> GetByIdAsync(int id);
        Task UpdateAsync(PostUpdateDto dto);
        Task DeleteAsync(int id);
    }
