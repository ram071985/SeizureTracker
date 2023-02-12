using Azure;
using Azure.Data.Tables;

namespace seizure_tracker.Service;

public class AzureTableService : IAzureTableService
{
    private readonly IConfiguration _config;
    private readonly string _azureConnectionString;
    private readonly string _tableName;
    public AzureTableService(IConfiguration config)
    {
        _config = config;
        _azureConnectionString = _config["AzureTable:ConnectionString"];
        _tableName = _config["AzureTable:TableName"];
    }

    private async Task<TableClient> GetTableClient()
    {
        TableServiceClient tableServiceClient = new TableServiceClient(_azureConnectionString);
        var tableClient = tableServiceClient.GetTableClient(_tableName);

        await tableClient.CreateIfNotExistsAsync();

        return tableClient;
    }

    public async Task<List<SeizureForm>> GetRecords(string queryFilter = "")
    {
        List<SeizureForm> seizureRecords = new();
        try
        {
            var tableClient = await GetTableClient();
            AsyncPageable<SeizureForm> records = tableClient.QueryAsync<SeizureForm>(filter: queryFilter);

            await foreach (SeizureForm entity in records)
            {
                seizureRecords.Add(entity);
            }

            seizureRecords = seizureRecords.OrderByDescending(x => DateTime.Parse(x.Date)).ThenByDescending(x => DateTime.Parse(x.TimeOfSeizure)).ToList();

            return seizureRecords;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }

    public async Task<List<SeizureForm>> GetRecordsByDate(string date)
    {
        List<SeizureForm> seizureRecords = new();
        SeizureForm seizureRecord = new();
        try
        {
            var tableClient = await GetTableClient();
            AsyncPageable<SeizureForm> records = tableClient.QueryAsync<SeizureForm>(x => x.Date == date);

            await foreach (SeizureForm entity in records)
            {
                seizureRecords.Add(entity);
            }

            return seizureRecords;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<SeizureForm> AddRecord(SeizureForm entity)
    {
        try
        {
            var tableClient = await GetTableClient();
            await tableClient.AddEntityAsync(entity);

            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }
}