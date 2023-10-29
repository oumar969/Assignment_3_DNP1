using Shared.Models;
using Shared.Models;
using Shared.Dtos;

namespace Application.LogicInterface;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(PostUpdateDto dto);
    Task DeleteAsync(int id);

    Task<PostBasicDto> GetByIdAsync(int id);
}