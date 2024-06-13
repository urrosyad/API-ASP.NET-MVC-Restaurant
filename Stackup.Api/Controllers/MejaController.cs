using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class MejaController : ControllerBase
{
    private readonly DbMeja _dbMeja;
    Response response = new Response();

    public MejaController(IConfiguration configuration)
    {
        _dbMeja = new DbMeja(configuration);
    }

    // GET: api/Meja
    [HttpGet("getAll")]
    public IActionResult GetAllMeja()
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbMeja.GetAllMeja();
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // POST / CREATE
    [HttpPost("postMeja")]
    public IActionResult CreateMeja([FromBody] Meja meja)
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbMeja.CreateMeja(meja);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // PUT / UPDATE
    [HttpPut("updateMeja/{id_meja}")]
    public IActionResult UpdateMeja(int id_meja, [FromBody] Meja meja)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbMeja.UpdateMeja(id_meja, meja);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }

    // DELETE 
    [HttpDelete("deleteMeja/{id_meja}")]
    public IActionResult DeleteMeja(int id_meja)
    {
        try{
            response.status = 200;
            response.message = "Success";
            _dbMeja.DeleteMeja(id_meja);
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
}

