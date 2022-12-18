import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { Observable, throwError } from 'rxjs';
import { MainForm } from 'src/models/mainForm.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';
import { FormBuilder, FormGroup, NgForm, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

export enum MedicationChange {
  Yes = 1,
  No = 0
}

@Component({
  selector: 'app-tracking-form',
  templateUrl: './tracking-form.component.html',
  styleUrls: ['./tracking-form.component.scss']
})
export class TrackingFormComponent implements OnInit {
  form: FormGroup;
  mainForm: MainForm;
  message: string = "";
  endpoint = '';
  timeRegEx: RegExp = new RegExp('^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$');
  strengthRegEx: RegExp = new RegExp('^[1-9][0-9]*$');
  time: string = '';
  seizureStrengthInput: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  amPMInput: string[] = ["AM", "PM"];
  ketoneLevelsInput: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  sleepInHoursInput: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  medicationChangeInput: string[] = ["TRUE", "FALSE"]
  seizureTypesInput: string[] = ["Partial", "Music", "Grand MAL", "Partial/Music", "Grand MAL/Music"];

  constructor(private httpClient: HttpClient, private builder: FormBuilder, private snackBar: MatSnackBar) {
    this.createSeizureForm();
  }

  @Input() toggled: "";
  @Output() toggledChange: EventEmitter<string> = new EventEmitter<string>();

  httpHeader = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  ngOnInit() {
  }

  // reg exprssion for HH:SS time
  // ^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$
  changeAmPm() {
    this.toggled = this.toggled;
    this.toggledChange.emit(this.toggled);
    console.log(this.toggled);
  }

  createSeizureForm() {
    this.form = this.builder.group({
      date: new FormControl([Validators.required]),
      timeOfSeizure: new FormControl("", this.timeValidator(this.timeRegEx)),
      seizureStrength: new FormControl(0, this.timeValidator(this.strengthRegEx)),
      medicationChange: "",
      medicationChangeExplanation: "",
      ketonesLevel: new FormControl(0, this.timeValidator(this.strengthRegEx)),
      seizureType: new FormControl("", [Validators.required]),
      sleepAmount: new FormControl(0, this.timeValidator(this.strengthRegEx)),
      amPM: new FormControl("", [Validators.required]),
      notes: ""
    })
  }

  get date() { return this.form.get('date') };
  get timeOfSeizure() { return this.form.get('timeOfSeizure') };
  get seizureStrength() { return this.form.get('seizureStrength') };
  get seizureType() { return this.form.get('seizureType') };
  get ketonesLevel() { return this.form.get('ketonesLevel') };
  get sleepAmount() { return this.form.get('sleepAmount') };
  get amPM() { return this.form.get('amPM') };
  get notes() { return this.form.get('notes') };

  timeValidator(timeRule: RegExp): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const forbidden = timeRule.test(control.value);
      return !forbidden ? { forbiddenName: { value: control.value } } : null;
    };
  }

  strengthValidator() {

  }

  seizureTypeValidator() {

  }

  ketonesLevelValidator() {

  }

  sleepAmountValidator() {

  }

  onSubmit() {
    this.addUser().subscribe((res: {}) => {
      this.message = res != null ? 'Record Successfully Submitted' : 'Something Went Wrong';
      this.snackBar.open(this.message, "CLOSE")
      this.form.reset();

    });
  }
  addUser(): Observable<any> {
    console.log('Your form data : ', this.form.value)

    return this.httpClient
      .post<any>(
        this.endpoint + '/seizuretracker',
        JSON.stringify(this.form.value),
        this.httpHeader
      )
      .pipe(retry(1), catchError(this.processError));
  }

  processError(err: any) {
    let message = '';
    if (err.error instanceof ErrorEvent) {
      message = err.error.message;
    } else {
      message = `Error Code: ${err.status}\nMessage: ${err.message}`;
    }
    console.log(message);
    return throwError(() => {
      message;
    });
  }



}