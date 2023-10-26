using Application.DaoInterfaces;
using Shared.Auth;
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