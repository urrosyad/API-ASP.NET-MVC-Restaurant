using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class ChefController : ControllerBase
{
    private readonly DbChef _dbChef;
    Response response = new Response();

    public ChefController(IConfiguration configuration)
    {
        _dbChef = new DbChef(configuration);
    }

    // GET: api/Chef
    [HttpGet("getAll")]
    public IActionResult GetAllChef()
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbChef.GetAllChef();
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // POST / CREATE
    [HttpPost("postChef")]
    public IActionResult CreateChef([FromBody] Chef chef)
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbChef.CreateChef(chef);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // PUT / UPDATE
    [HttpPut("updateChef/{id_chef}")]
    public IActionResult UpdateChef(int id_chef, [FromBody] Chef chef)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbChef.UpdateChef(id_chef, chef);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // DELETE 
    [HttpDelete("deleteChef/{id_chef}")]
    public IActionResult DeleteChef(int id_chef)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbChef.DeleteChef(id_chef);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
}

