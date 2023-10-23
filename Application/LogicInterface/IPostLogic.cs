using Shared.Auth;
using Shared.Dtos;

namespace Application.LogicInterface;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);

}