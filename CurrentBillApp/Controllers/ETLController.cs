using Microsoft.AspNetCore.Mvc;

namespace CurrentBillApp.Controllers
{
  [ApiController]
  [Route("api/etl")]
  public class ETLController : ControllerBase
  {
    private readonly ETLService _etlService;

    public ETLController(ETLService etlService)
    {
      _etlService = etlService;
    }

    [HttpPost("run")]
    public IActionResult RunETL()
    {
      try
      {
        _etlService.RunETL();
        return Ok(new { Message = "ETL process completed successfully." });
      }
      catch (Exception ex)
      {
        Console.WriteLine($"ETL Error: {ex.Message}");
        return StatusCode(500, new { Message = "ETL process failed.", Error = ex.Message });
      }
    }

  }
}
