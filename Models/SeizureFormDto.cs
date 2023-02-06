public class SeizureFormDto
{
    public string Date { get; init; } = default!;
    public string TimeOfSeizure { get; init; }
    public int SeizureStrength { get; init; }
    public string MedicationChange { get; set; }
    public string MedicationChangeExplanation { get; set; }
    public string KetonesLevel { get; set; }
    public string SeizureType { get; set; }
    public int SleepAmount { get; set; }
    public string AmPm { get; set; }
    public string Notes { get; set; }
}