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

            var groups = groupByDate.ToList();

            var pageSize = 10;

            var skip = pageSize * (pageNumber - 1);

            seizures.Seizures = groupByDate.Select(x => x).Skip(skip).Take(pageSize).ToList();

            var pageCount = groupByDate.Count() > 10 ? (double)groupByDate.Count() / 10 : 1;

            seizures.PageCount = pageCount;

            return seizures;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    private Tuple<int, int> setIndexes(int pageNumber)
    {
        var topIndex = pageNumber * 10 - 1;

        var bottomIndex = pageNumber * 10 - 10;

        return new Tuple<int, int>(topIndex, bottomIndex);
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

    private async Task<List<SeizureForm>> getRecords() => await _azureTableService.GetRecords();
    private async Task<SeizureForm> addRecord(SeizureForm form) => await _azureTableService.AddRecord(form);


}