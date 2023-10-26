using Application.DaoInterfaces;
using Application.LogicInterface;
using Shared.Auth;
using Shared.Dtos;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;


    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
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
        Post post = new Post(user, dto.Title, dto.Body);
        Post created = await postDao.CreateAsync(post);
        return created;
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task<PostBasicDto?> GetByIdAsync(int postId)
    {
        Post? post = await postDao.GetByIdAsync(postId);
        if (post == null)
        {
            throw new Exception($"Post with id {postId} not found");
        }

        return new PostBasicDto(post.PostId, post.Owner.Username, post.Title, post.Body);
        
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

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
        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string bodyToUse = dto.Body ?? existing.Body;

        if (bodyToUse.Length > 100)
        {
            throw new Exception("Body cannot be more than 100 characters!");
        }
        
        Post updated = new (userToUse, titleToUse,bodyToUse)
        {
            PostId = existing.PostId,
        };

        ValidateTodo(updated);

        await postDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Post? todo = await postDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        await postDao.DeleteAsync(id);
    }
    
    private void ValidateTodo(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
        // other validation stuff
    }
    
    private void ValidateTodo(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        
        if (dto.Title.Length > 100) throw new Exception("Title cannot be longer than 100 characters.");

        if (dto.Title.Length < 5) throw new Exception("Title cannot be shorter than 5 characters.");
        if (dto.Title == null) throw new Exception("Title cannot be null.");
        if (dto.Title == "") throw new Exception("Title cannot be empty.");
        if (dto.Body.Length > 1000) throw new Exception("Body cannot be longer than 1000 characters.");
        if (dto.Owner == null) throw new Exception("Owner cannot be null.");
        if (dto.Body.Length < 5) throw new Exception("Body cannot be shorter than 5 characters.");
        if (dto.Body == null) throw new Exception("Body cannot be null.");
        if (dto.Body == "") throw new Exception("Body cannot be empty.");
        }
}