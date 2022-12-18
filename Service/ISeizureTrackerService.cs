namespace seizure_tracker.Service;

public interface ISeizureTrackerService
{
    public Task<SeizureFormDto[]> GetRecords();
    public Task<SeizureFormDto> AddRecord(SeizureFormDto form);
}