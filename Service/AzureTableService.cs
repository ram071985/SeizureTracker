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

    public async Task<SeizureForm[]> GetRecords()
    {
        List<SeizureForm> seizureRecords = new();

        try
        {
            var tableClient = await GetTableClient();
            AsyncPageable<SeizureForm> records = tableClient.QueryAsync<SeizureForm>(filter: "");

            await foreach (SeizureForm entity in records)
            {
                seizureRecords.Add(entity);
            }
            seizureRecords = seizureRecords.OrderByDescending(x => DateTime.Parse(x.Date)).ThenByDescending(x => DateTime.Parse(x.TimeOfSeizure)).ToList();
            return seizureRecords.ToArray();
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