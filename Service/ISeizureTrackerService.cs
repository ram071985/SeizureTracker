namespace seizure_tracker.Service;

public interface ISeizureTrackerService
{
    public Task<IEnumerable<SeizureFormDto[]>> GetRecords();
    public Task<SeizureFormDto> AddRecord(SeizureFormDto form);
}