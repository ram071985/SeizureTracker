import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';
import { FooterComponent } from './footer/footer.component';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgMaterialModule } from './ng-material/ng-material.module';
import { TrackingFormComponent } from './shared/components/tracking-form/tracking-form.component';
import { AppRoutingModule } from './app-routing.module';
import { RecordsComponent } from './shared/components/records/records.component';
import { RecordTableComponent } from './shared/components/records/record-table/record-table.component';
import { RecordTableItemsComponent } from './shared/components/records/record-table-items/record-table-items.component';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    TrackingFormComponent,
    RecordsComponent,
    RecordTableComponent,
    RecordTableItemsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: MainComponent },
      { path: 'records', component: RecordsComponent },
    ]),
    BrowserAnimationsModule,
    NgMaterialModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
