import { Component, ViewChild } from '@angular/core';
import { Chart, ChartConfiguration, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { SeizureReturn } from 'src/models/seizureReturn.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MainForm } from 'src/models/mainForm.model';

@Component({
    selector: 'app-data',
    templateUrl: './data.component.html',
    styleUrls: ['./data.component.scss']
})
export class DataComponent {
    endpoint: string = '';
    loading: boolean;
    seizureRecords: MainForm[];

    public lineChartData: ChartConfiguration['data'] = {
        datasets: [
            {
                data: [65, 59, 80, 81, 56, 55, 40],
                label: 'Series A',
                backgroundColor: 'rgba(148,159,177,0.2)',
                borderColor: 'rgba(148,159,177,1)',
                pointBackgroundColor: 'rgba(148,159,177,1)',
                pointBorderColor: '#fff',
                pointHoverBackgroundColor: '#fff',
                pointHoverBorderColor: 'rgba(148,159,177,0.8)',
                fill: 'origin',
            },
        ],
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
    };

    public lineChartType: ChartType = 'line';

    @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

    constructor(private httpClient: HttpClient) {
    }

    httpHeader = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        }),
    };

    ngOnInit() {
        this.loading = false;
        this.getRecords().subscribe((res: SeizureReturn) => {
            this.loading = false;

            this.seizureRecords = res.seizures;
            console.log('Seizure Records: ', this.seizureRecords);
        });
    }

    getRecords(): Observable<SeizureReturn> {
        //  this.loading = true;
        return this.httpClient
            .get<any>(
                this.endpoint + '/seizuretracker/records',
                this.httpHeader
            )
            .pipe(retry(1), catchError(err => { throw 'The query failed. Details: ' + err }));
    }
}