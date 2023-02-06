
using Azure;
using Azure.Data.Tables;

public record SeizureForm : ITableEntity
{
    public string RowKey { get; set; } = default!;
    public string PartitionKey { get; set; } = default!;
    public string Date { get; init; } = default!;
    public string TimeOfSeizure { get; init; }
    public int SeizureStrength { get; init; }
    public string MedicationChange { get; set; }
    public string MedicationChangeExplanation { get; set; }
    public double KetonesLevel { get; set; } 
    public string SeizureType { get; set; }
    public int SleepAmount { get; set; }
    public string AmPm { get; set; }
    public ETag ETag { get; set; } = default!;
    public DateTimeOffset? Timestamp { get; set; } = default!;
    public string Notes { get; set; }
}