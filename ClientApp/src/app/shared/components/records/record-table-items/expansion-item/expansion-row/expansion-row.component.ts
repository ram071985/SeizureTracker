import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MainForm } from 'src/models/mainForm.model';

@Component({
    selector: 'app-expansion-row',
    templateUrl: './expansion-row.component.html',
    styleUrls: ['./expansion-row.component.scss']
})

export class ExpansionRowComponent {
    @Input() row: string = '';
    @Input() item: MainForm;
}