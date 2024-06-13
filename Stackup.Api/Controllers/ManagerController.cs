using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class ManagerController : ControllerBase
{
    private readonly DbManager _dbManager;
    Response response = new Response();

    public ManagerController(IConfiguration configuration)
    {
        _dbManager = new DbManager(configuration);
    }

    // GET: api/Manager
    [HttpGet("getAll")]
    public IActionResult GetAllManager()
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbManager.GetAllManager();
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // POST / CREATE
    [HttpPost("postManager")]
    public IActionResult CreateManager([FromBody] Manager manager)
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbManager.CreateManager(manager);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // PUT / UPDATE
    [HttpPut("updateManager/{id_manager}")]
    public IActionResult UpdateMenu(int id_manager, [FromBody] Manager manager)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbManager.UpdateManager(id_manager, manager);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // DELETE 
    [HttpDelete("deleteManager/{id_manager}")]
    public IActionResult DeleteManager(int id_manager)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbManager.DeleteManager(id_manager);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
}

