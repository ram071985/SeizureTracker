
namespace seizure_tracker.Service;

public interface IAzureTableService
{
    public Task<List<SeizureForm>> GetRecords(string queryFilter = "");
    public Task<SeizureForm> AddRecord(SeizureForm entity);
    public Task<List<SeizureForm>> GetRecordsByDate(string date);
}