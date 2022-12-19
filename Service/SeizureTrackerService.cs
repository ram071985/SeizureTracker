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

    public async Task<IEnumerable<SeizureFormDto[]>> GetRecords()
    {
        IEnumerable<SeizureFormDto[]> seizureForm = Enumerable.Empty<SeizureFormDto[]>();

        try
        {
            var records = await getRecords();

            if (!records.Any())
                return seizureForm;

            var parseRecords = records.Select(r => r.MapToSeizureFormDto()).ToArray();

            return parseRecords.GroupBy(r => r.Date).Select(g => g.ToArray());
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

    private async Task<SeizureForm[]> getRecords() => await _azureTableService.GetRecords();
    private async Task<SeizureForm> addRecord(SeizureForm form) => await _azureTableService.AddRecord(form);


}