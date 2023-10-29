using Shared.Models;
using Shared.Dtos;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task<Post?> GetByIdAsync(int postId);
    Task UpdateAsync(Post dto);
    Task DeleteAsync(int id);

}

