
namespace seizure_tracker.Service;

public interface IAzureTableService
{
    public Task<List<SeizureForm>> GetRecords();
    public Task<SeizureForm> AddRecord(SeizureForm entity);
}