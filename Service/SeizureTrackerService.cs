using seizure_tracker.Service.Mappings;
namespace seizure_tracker.Service;

public class SeizureTrackerService : ISeizureTrackerService
{
    private readonly IConfiguration _config;
    private readonly IAzureTableService _azureTableService;
    private string _filter;
    private int _pageCount;

    #region  Construction
    public SeizureTrackerService(IConfiguration config, IAzureTableService azureTableService)
    {
        _config = config;
        _azureTableService = azureTableService;

        _filter = "";
        _pageCount = int.Parse(_config["Pagination:PageCount"]);
    }
    #endregion

    #region Public Methods
    public async Task<SeizureFormReturn> GetPaginatedRecords(int pageNumber = 1)
    {
        SeizureFormReturn seizures = new();
        try
        {

            var parseRecords = await mapRecords();

            return paginateRecords(parseRecords, pageNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<List<(DateTime, int)>> GetFilteredRecords()
    {
        List<(DateTime, int)> totalSeizureSet = new();

        try
        {
            var parseRecords = await mapRecords();

            var groupByDate = parseRecords.GroupBy(r => r.Date).Select(g => g.ToList());

            var groups = groupByDate.ToList();

            totalSeizureSet = await filterSeizureCount(groups, totalSeizureSet.OrderBy(x => x.Item1).ToList());

            return totalSeizureSet;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        }
    }

    public async Task<SeizureFormDto> CheckForKetones(string date)
    {
        SeizureFormDto seizure = new();

        _filter = $"Date eq datetime'{date}'";

        try
        {
            var records = await getDateRecords(date);

            if (!records.Any())
                return seizure;

            var parseRecords = records.Select(r => r.MapToSeizureFormDto()).ToList();

            seizure = parseRecords.OrderByDescending(x => double.Parse(x.KetonesLevel)).FirstOrDefault();

            return seizure;
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
    #endregion
    #region Private Methods
    private IEnumerable<DateTime> eachDay(DateTime from, DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            yield return day;
    }

    private async Task<List<(DateTime, int)>> filterSeizureCount(List<List<SeizureFormDto>> groups, List<(DateTime, int)> totalSeizureSet)
    {
        foreach (var date in groups)
        {
            totalSeizureSet.Add((DateTime.Parse(date[0].Date), date.Count));
        }

        for (int i = 0; i < totalSeizureSet.Count - 1; i++)
        {
            DateTime currrent = totalSeizureSet[i].Item1;
            DateTime next = totalSeizureSet[i + 1].Item1;
            DateTime expected = currrent.AddDays(1);

            if (next != expected)
            {
                totalSeizureSet.Insert(++i, (expected, 0));
            }
        }
        return totalSeizureSet;
    }

    private async Task<List<SeizureFormDto>> mapRecords()
    {
        List<SeizureFormDto> seizures = new();

        try
        {
            var records = await getRecords(_filter);

            if (!records.Any())
                return seizures;

            return records.Select(r => r.MapToSeizureFormDto()).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    private SeizureFormReturn paginateRecords(List<SeizureFormDto> records, int pageNumber)
    {
        SeizureFormReturn seizures = new();
        seizures.Seizures = new();

        var groupByDate = records.GroupBy(r => r.Date).Select(g => g.ToList());

        var groups = groupByDate.ToList();

        var skip = _pageCount * (pageNumber - 1);

        seizures.Seizures = groupByDate.Select(x => x).Skip(skip).Take(_pageCount).ToList();

        var pageCount = groupByDate.Count() > 10 ? (double)groupByDate.Count() / 10 : 1;

        seizures.PageCount = pageCount;

        return seizures;
    }
    private async Task<List<SeizureForm>> getRecords(string queryFilter) => await _azureTableService.GetRecords(queryFilter);
    private async Task<List<SeizureForm>> getDateRecords(string date) => await _azureTableService.GetRecordsByDate(date);
    private async Task<SeizureForm> addRecord(SeizureForm form) => await _azureTableService.AddRecord(form);
    #endregion
}