using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly DbMenu _dbMenu;
    Response response = new Response();

    public MenuController(IConfiguration configuration)
    {
        _dbMenu = new DbMenu(configuration);
    }

    // GET: api/Menu

    [HttpGet("getAll")]

    public IActionResult GetAllMenu()
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbMenu.GetAllMenu();
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // POST / CREATE
    [HttpPost("postMenu")]
    public IActionResult CreateMenu([FromBody] Menu menu)
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbMenu.CreateMenu(menu);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // PUT / UPDATE
    [HttpPut("updateMenu/{id_menu}")]
    public IActionResult UpdateMenu(int id_menu, [FromBody] Menu menu)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbMenu.UpdateMenu(id_menu, menu);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // DELETE 
    [HttpDelete("deleteMenu/{id_menu}")]
    public IActionResult DeleteMenu(int id_menu)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbMenu.DeleteMenu(id_menu);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
}

