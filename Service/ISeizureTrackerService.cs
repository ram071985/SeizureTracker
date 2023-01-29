namespace seizure_tracker.Service;

public interface ISeizureTrackerService
{
    public Task<SeizureFormReturn> GetRecords(int pageNumber = 1);
    public Task<SeizureFormDto> AddRecord(SeizureFormDto form);
}