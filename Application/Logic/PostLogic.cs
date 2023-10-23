using Application.DaoInterfaces;
using Application.LogicInterface;
using Shared.Auth;
using Shared.Dtos;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;


    public PostLogic(IPostDao postDao)
    {
        this.postDao = postDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
        
        Post post = new Post(user, dto.Title);
        ValidateTodo(post);
        Post created = await postDao.CreateAsync(post);
        return created;
    }
    
    private void ValidateTodo(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        else
        {
            if (dto.Title.Length > 100) throw new Exception("Title cannot be longer than 100 characters.");
        }
        
    }
}