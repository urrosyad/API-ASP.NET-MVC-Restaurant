using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class PesananController : ControllerBase
{
    private readonly DbPesanan _dbpesanan;
    Response response = new Response();

    public PesananController(IConfiguration configuration)
    {
        _dbpesanan = new DbPesanan(configuration);
    }

    // GET: api/Meja
    [HttpGet("getAllPesanan")]
    public IActionResult GetAllPesanan()
    {
        try{
            response.status = 200;
            response.message = "Success";
            response.data = _dbpesanan.GetAllPesanan();
        }
        catch (Exception ex)
        {
            response.status = 500;
            response.message = ex.Message;
        }
        return Ok(response);
    }
}