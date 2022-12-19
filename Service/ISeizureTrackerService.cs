namespace seizure_tracker.Service;

public interface ISeizureTrackerService
{
    public Task<SeizureFormReturnModel[]> GetRecords();
    public Task<SeizureFormDto> AddRecord(SeizureFormDto form);
}