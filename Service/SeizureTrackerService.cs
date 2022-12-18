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

    public async Task<SeizureFormDto[]> GetRecords()
    {
        List<SeizureFormDto> seizureForm = new();

        try
        {
            var records = await getRecords();

            if (!records.Any())
                return seizureForm.ToArray();

            return records.Select(r => r.MapToSeizureFormDto()).ToArray();
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