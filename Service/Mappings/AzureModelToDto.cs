namespace seizure_tracker.Service.Mappings;

internal static class AzureModelToDto
{
    internal static SeizureFormDto MapToSeizureFormDto(this SeizureForm form)
    {
        return new()
        {
            Date = form.Date,
            TimeOfSeizure = form.TimeOfSeizure,
            SeizureStrength = form.SeizureStrength,
            SeizureType = form.SeizureType,
            MedicationChange = form.MedicationChange,
            MedicationChangeExplanation = form.MedicationChangeExplanation,
            KetonesLevel = form.KetonesLevel.ToString("0.0"),
            SleepAmount = form.SleepAmount,
            Notes = form.Notes,
            AmPm = form.AmPm
        };
    }
}