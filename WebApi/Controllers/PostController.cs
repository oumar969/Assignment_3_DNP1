using Application.LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.Dtos;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class PostController: ControllerBase 
{
    private readonly IPostLogic postLogic;  //readonly betyder, at vi ikke kan ændre på den.

    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    
    [HttpPost] 
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto) //acync betyder, at vi skal bruge en async metode. //[FromBody] betyder, at vi skal bruge en body i vores request.
    {       //async er en metode, som ikke blokerer vores program, mens den kører.
        try
        {
            Post created = await postLogic.CreateAsync(dto);
            return Created($"/posts/{created.PostId}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] string? titleContains)
    {
        try
        {
            SearchPostParametersDto parameters = new(userName, userId, titleContains/*, bodyContains*/);
            var posts = await postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
    {
        try
        {
            await postLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await postLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            PostBasicDto result = await postLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


}