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

    [HttpPost("records")]
    public async Task<SeizureFormReturn> GetSeizureRecords([FromBody] int page = 1)
    {
        try
        {
            var records = await _seizureTrackerService.GetPaginatedRecords(page);


            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        }

    }

    [HttpGet("data")]
    public async Task<List<(DateTime, int)>> GetFilteredRecords()
    {
        try
        {
            var records = await _seizureTrackerService.GetFilteredRecords();

            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        }

    }

    [HttpPost("check_ketones")]
    public async Task<SeizureFormDto> CheckKetonesLevels([FromBody] string date)
    {
        try
        {
            var record = await _seizureTrackerService.CheckForKetones(date);

            return record;
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
