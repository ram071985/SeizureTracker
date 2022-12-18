using Azure.Data.Tables;
using seizure_tracker.Service;
using Microsoft.AspNetCore.Mvc;
using Azure;

namespace seizure_tracker.Controllers;

[Route("[controller]")]
[ApiController]
public class SeizureTrackerController : ControllerBase
{

    private readonly ILogger<SeizureTrackerController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ISeizureTrackerService _seizureTrackerService;


    public SeizureTrackerController(ILogger<SeizureTrackerController> logger, IConfiguration configuration, ISeizureTrackerService seizureTrackerService)
    {
        _logger = logger;
        _configuration = configuration;
        _seizureTrackerService = seizureTrackerService;
    }

    [HttpGet]
    public async Task<SeizureFormDto[]> GetSeizureRecords()
    {
        try
        {
            var records = await _seizureTrackerService.GetRecords();

            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        }

    }
    [HttpPost]
    public async Task<SeizureFormDto> CreateMainFormRecord([FromBody] SeizureFormDto form)
    {
        try
        {
            var record = await _seizureTrackerService.AddRecord(form);

            return record;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
