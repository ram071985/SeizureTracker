namespace seizure_tracker.Service;

public interface ISeizureTrackerService
{
    public Task<SeizureFormReturn> GetPaginatedRecords(int pageNumber = 1);
    public Task<SeizureFormDto> AddRecord(SeizureFormDto form);
    public Task<SeizureFormDto> CheckForKetones(string date);
    public Task<SeizureFormReturn> GetFilteredRecords();
}