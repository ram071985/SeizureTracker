import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TrackingFormComponent } from './shared/components/tracking-form/tracking-form.component';

const routes: Routes = [
  { path: '/', component: TrackingFormComponent },
  
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AppRoutingModule { }
