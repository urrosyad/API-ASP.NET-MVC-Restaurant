using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class WaitressController : ControllerBase
{
    private readonly DbWaitress _dbWaitress;
    Response response = new Response();

    public WaitressController(IConfiguration configuration)
    {
        _dbWaitress = new DbWaitress(configuration);
    }

    // GET: api/Waitress
    [HttpGet("getAll")]
    public IActionResult GetAllWaitress()
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbWaitress.GetAllWaitress();
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // POST / CREATE
    [HttpPost("postWaitress")]
    public IActionResult CreateWaitress([FromBody] Waitress waitress)
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbWaitress.CreateWaitress(waitress);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // PUT / UPDATE
    [HttpPut("updateWaitress/{id_waitress}")]
    public IActionResult UpdateWaitress(int id_waitress, [FromBody] Waitress waitress)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbWaitress.UpdateWaitress(id_waitress, waitress);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // DELETE 
    [HttpDelete("deleteWaitress/{id_waitress}")]
    public IActionResult DeleteWaitress(int id_waitress)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbWaitress.DeleteWaitress(id_waitress);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
}

