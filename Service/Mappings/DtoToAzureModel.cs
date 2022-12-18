namespace seizure_tracker.Service.Mappings;

internal static class DtoToAzureModel
{
    internal static SeizureForm MapDtoToAzureModel(this SeizureFormDto form)
    {
        return new()
        {
            PartitionKey = form.SeizureType,
            RowKey = Guid.NewGuid().ToString(),
            Date = form.Date,
            TimeOfSeizure = form.TimeOfSeizure,
            SeizureStrength = form.SeizureStrength,
            SeizureType = form.SeizureType,
            MedicationChange = form.MedicationChange,
            MedicationChangeExplanation = form.MedicationChangeExplanation,
            KetonesLevel = form.KetonesLevel,
            SleepAmount = form.SleepAmount,
            Notes = form.Notes,
            AmPm = form.AmPm
        };
    }
}