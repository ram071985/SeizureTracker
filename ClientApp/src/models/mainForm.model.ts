

export interface MainForm {
    rowKey?: string,
    partitionKey?: string,
    date: string,
    timeOfSeizure: string,
    seizureStrength: number,
    medicationChange: string,
    medicationChangeExplanation: string,
    ketonesLevel: number,
    seizureType: string,
    sleepAmount: number,
    amPM: string,
    notes: string
}