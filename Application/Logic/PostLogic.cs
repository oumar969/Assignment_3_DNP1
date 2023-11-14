using Application.DaoInterfaces;
using Application.LogicInterface;
using Shared.Models;
using Shared.Dtos;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private  readonly IPostDao _postDao;
    private  readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this._postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidateTodo(dto);
        Post post = new Post(user.Id, dto.Title, dto.Body);
        Post created = await _postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return _postDao.GetAsync(searchParameters);
    }

    private void ValidateTodo(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
        
    }

    public  async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await _postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        int userToUse = existing?.OwnerId ?? 0; // antager, at OwnerId er af typen int
        string titleToUse = dto.Title ?? existing.Title;
        string bodyToUse = dto.Body ?? existing.Body;

        if (bodyToUse.Length > 100)
        {
            throw new Exception("Body cannot be more than 100 characters!");
        }
        
        Post updated = new (userToUse, titleToUse,bodyToUse)
        {
            Id = existing.Id,
        };

        ValidateTodo(updated);

        await _postDao.UpdateAsync(updated);
    }

    public  async Task DeleteAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        await _postDao.DeleteAsync(id);
    }

    public  async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new PostBasicDto(post.Id, post.Owner.UserName, post.Title, post.Body); //,todo.IsCompleted);
    }
    
    private  void ValidateTodo(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
        // other validation stuff
    }
}