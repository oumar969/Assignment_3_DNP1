using System.ComponentModel.DataAnnotations;
using FileData;
using Shared.Auth;

namespace WebApi.Services;

public class AuthService : IAuthService
{
    private FileContext file = new FileContext();
    private readonly IList<User> Users = new List<User>();

    public AuthService()
    {
        file.LoadData();
    }

    public Task<User> ValidateUser(string username, string password)
    {
        file.LoadData();
        Console.WriteLine(username+password);
        User? existingUser = file.Users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }
    

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        file.Users.Add(user);
        file.SaveChanges();
        
        return Task.CompletedTask;
    }
}