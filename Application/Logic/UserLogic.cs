using Application.DaoInterfaces;
using Application.LogicInterface;
using Shared.Dtos;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName, Password = dto.Password,Role = dto.Role
        };
        
        User created = await userDao.CreateAsync(toCreate);
        
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public Task<User> UpdateAsync(int id, UserUpdateDto dto)
    {
        User toUpdate = new User
        {
            Id = id, UserName = dto.UserName, Password = dto.Password,Role = dto.Role
        };
        
        return userDao.UpdateAsync(toUpdate);
    }

    public async Task DeleteAsync(int id)
    {
        User? user = await userDao.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }

        await userDao.DeleteAsync(id);
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
        string passWord = userToCreate.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        if (passWord.Length < 5)
            throw new Exception("Password must be at least 5 characters!");

        if (passWord.Length > 15)
            throw new Exception("Password must be less than 16 characters!");

        if (userName.Contains(" "))
            throw new Exception("Username cannot contain spaces!");
        
        if(passWord.Contains(" "))
            throw new Exception("Password cannot contain spaces!");
        
        if (userName.Contains("!"))
            throw new Exception("Username cannot contain exclamation marks!");
        
        if(passWord.Contains("!"))
            throw new Exception("Password cannot contain exclamation marks!");
        
    }
}