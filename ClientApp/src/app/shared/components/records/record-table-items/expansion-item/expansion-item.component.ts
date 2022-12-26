import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MainForm } from 'src/models/mainForm.model';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-expansion-item',
    templateUrl: './expansion-item.component.html',
    styleUrls: ['./expansion-item.component.scss']
})

export class ExpansionItemComponent {
    @Input() item: MainForm;
    @Input() recordNumber: number;
    displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
    rows: string[] = ['Date', 'Time', 'Seizure Strength', 'Medication Change', 'M.C. Explanation', 'Ketones Level', 'Seizure Type', 'Sleep Amount', 'Notes'];

    constructor(datePipe: DatePipe)
    {

    }
}