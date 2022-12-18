
namespace seizure_tracker.Service;

public interface IAzureTableService
{
    public Task<SeizureForm[]> GetRecords();
    public Task<SeizureForm> AddRecord(SeizureForm entity);
}