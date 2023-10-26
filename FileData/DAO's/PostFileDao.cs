using Application.DaoInterfaces;
using Shared.Auth;
using Shared.Dtos;

namespace FileData.DAO_s;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.PostId);
            id++;
        }

        post.PostId = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            result = context.Posts.Where(todo =>
                todo.Owner.Username.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchParameters.UserId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        // if (!string.IsNullOrEmpty(searchParameters.BodyContains))
        // {
        //     result = result.Where(t =>
        //         t.Body.Contains(searchParameters.BodyContains, StringComparison.OrdinalIgnoreCase));
        // }
        return Task.FromResult(result);
    }

    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.PostId == postId);
        return Task.FromResult(existing);    }

    public Task UpdateAsync(Post dto)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == dto.PostId);
        if (existing == null)
        {
            throw new Exception($"Todo with id {dto.PostId} does not exist!");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(dto);
        
        context.SaveChanges();
        
        return Task.CompletedTask;    }

    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(todo => todo.PostId == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        context.SaveChanges();

        return Task.CompletedTask;    }

    public Task<IEnumerable<Post>> GetAsync()
    {
        var result = context.Posts.AsEnumerable();
        return Task.FromResult(result);
    }
}