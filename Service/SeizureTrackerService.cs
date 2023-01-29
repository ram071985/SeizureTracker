using seizure_tracker.Service.Mappings;
namespace seizure_tracker.Service;

public class SeizureTrackerService : ISeizureTrackerService
{
    private readonly IConfiguration _config;
    private readonly IAzureTableService _azureTableService;


    public SeizureTrackerService(IConfiguration config, IAzureTableService azureTableService)
    {
        _config = config;
        _azureTableService = azureTableService;
    }

    public async Task<SeizureFormReturn> GetRecords(int pageNumber)
    {
        SeizureFormReturn seizures = new();
        seizures.Seizures = new();

        try
        {
            var records = await getRecords();

            if (!records.Any())
                return seizures;

            var parseRecords = records.Select(r => r.MapToSeizureFormDto()).ToList();

            var groupByDate = parseRecords.GroupBy(r => r.Date).Select(g => g.ToList());

            var pageCount = groupByDate.Count() > 10 ? groupByDate.Count() / 10 : 1;
            seizures.PageCount = pageCount;

            var groups = groupByDate.ToList();

            var iteration = pageNumber != 1 ? pageNumber * 10 - 10 : 0;

            for (var i = iteration; i < iteration + 10; i++)
            {
                seizures.Seizures.Add(groups[i]);
            }

            return seizures;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<SeizureFormDto> AddRecord(SeizureFormDto form)
    {
        try
        {
            var record = form.MapDtoToAzureModel();

            await addRecord(record);

            return form;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    // private async Task<SeizureForm[]> getRecords() => await _azureTableService.GetRecords();

    private async Task<List<SeizureForm>> getRecords() => await _azureTableService.GetRecords();
    private async Task<SeizureForm> addRecord(SeizureForm form) => await _azureTableService.AddRecord(form);


}