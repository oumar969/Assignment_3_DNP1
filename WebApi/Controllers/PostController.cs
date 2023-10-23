using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]// vi bruger ApiController, fordi vi skal bruge http requests.
[Route("[controller]")]// vi bruger Route, fordi vi skal bruge en route til vores http requests.

public class PostController: ControllerBase 
{
    private readonly IPostLogic todoLogic;//readonly betyder, at vi ikke kan ændre på den.

    public PostController(IPostLogic todoLogic)
    {
        this.todoLogic = todoLogic;
    }
    
    [HttpPost] // vi bruger HttpPost, fordi vi skal bruge en post request.
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto) //acync betyder, at vi skal bruge en async metode. //[FromBody] betyder, at vi skal bruge en body i vores request.
    {//async er en metode, som ikke blokerer vores program, mens den kører.
        try
        {
            Post created = await todoLogic.CreateAsync(dto);
            return Created($"/posts/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }}
}