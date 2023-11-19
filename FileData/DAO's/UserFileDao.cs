using Application.DaoInterfaces;
using Shared.Models;
using Shared.Dtos;

namespace FileData.DAO_s;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string Username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(Username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Id == id
        );
        return Task.FromResult(existing);
    }

    public Task<User> UpdateAsync(User user)
    {
User? existing = context.Users.FirstOrDefault(u =>
            u.Id == user.Id
        );

        if (existing == null)
        {
            throw new Exception($"User with ID {user.Id} not found!");
        }

        existing.UserName = user.UserName;
        existing.Password = user.Password;
        existing.Role = user.Role;

        context.SaveChanges();

        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Id == id
        );

        if (existing == null)
        {
            throw new Exception($"User with ID {id} not found!");
        }

        context.Users.Remove(existing);
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(context.Users);
    }
}