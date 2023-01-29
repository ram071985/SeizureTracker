

export interface MainForm {
    rowKey?: string,
    partitionKey?: string,
    date: string,
    timeOfSeizure: string,
    seizureStrength: any,
    medicationChange: string,
    medicationChangeExplanation: string,
    ketonesLevel: any,
    seizureType: string,
    sleepAmount: any,
    amPm: string,
    notes: string,
    pageCount?: number,
    pageNumber?: number
}