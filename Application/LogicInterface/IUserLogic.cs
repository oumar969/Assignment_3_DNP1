using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterface;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    public Task<User> UpdateAsync(int id, UserUpdateDto dto);
    public Task DeleteAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();

}