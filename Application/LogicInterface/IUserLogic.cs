using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterface;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}