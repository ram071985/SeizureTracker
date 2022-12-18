import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { Observable, throwError } from 'rxjs';
import { MainForm } from 'src/models/mainForm.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';
import { FormBuilder, FormGroup, NgForm, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DatePipe } from '@angular/common';

export interface PeriodicElement {
    name: string;
    position: number;
    weight: number;
    symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
    { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
    { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
    { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
    { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
    { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
    { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
    { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
    { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
    { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
    { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];

@Component({
    selector: 'app-record-table',
    templateUrl: './record-table.component.html',
    styleUrls: ['./record-table.component.scss']
})

export class RecordTableComponent implements OnInit {
    displayedColumns: string[] = ['date', 'time', 'seizureStrength', 'medicationChange', 'medicationChangeExplanation', 'ketonesLevel', 'seizureType', 'sleepAmount', 'notes'];
    endpoint = '';
    seizureRecords: MainForm[];
    loading: boolean;

    constructor(private httpClient: HttpClient, private snackBar: MatSnackBar, private datePipe: DatePipe) {
    }

    httpHeader = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        }),
    };

    ngOnInit() {
        this.loading = false;
        this.getRecords().subscribe((records: MainForm[]) => {
            this.loading = false;
            this.seizureRecords = records;

            console.log('Seizure Records: ', this.seizureRecords);
        });
        

    }

    getRecords(): Observable<MainForm[]> {
        this.loading = true;
        return this.httpClient
            .get<MainForm[]>(
                this.endpoint + '/seizuretracker',
            )
            .pipe(retry(1), catchError(err => { throw 'The query failed. Details: ' + err }));
    }
}