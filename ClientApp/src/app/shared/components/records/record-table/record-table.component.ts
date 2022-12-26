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



@Component({
    selector: 'app-record-table',
    templateUrl: './record-table.component.html',
    styleUrls: ['./record-table.component.scss']
})

export class RecordTableComponent implements OnInit {
    displayedColumns: string[] = ['date', 'time', 'seizureStrength', 'medicationChange', 'medicationChangeExplanation', 'ketonesLevel', 'seizureType', 'sleepAmount', 'notes'];
    endpoint = '';
    seizureRecords: MainForm[];
    mappedRecords: {"id": string, "value": string}[];
    loading: boolean;
    panelOpenState = false;
    map = new Map<string, string>();

    expansionDateColumn = 'DATE';
    expansionCountColumn = 'SEIZURE COUNT';

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

            // this.seizureRecords.map((x) => {
            // this.seizureRecords.map((item, i) => {
            //     this.mappedRecords.push({ name: Object.keys(this.convertFromCamelCase(item[i])), value: item })
            // })
        //    const dick = this.seizureRecords.forEach((...[key, value]) => key[value].forEach((...[key, value]) => {
        //     console.log(key)
        //    }))
            
            console.log('Seizure Records: ', this.seizureRecords);
        });


    }

    convertFromCamelCase(text: string) {
        const result = text.replace(/([A-Z])/g, " $1");
        const finalResult = result.charAt(0).toUpperCase() + result.slice(1);
        return finalResult;
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