import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { API_BASE_URL } from './web-api-client';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { HotelsListComponent } from './hotels/hotels-list/hotels-list.component';
import { OffersListComponent } from './hotels/hotels-list/hotel-offers-list/offers-list.component';


@NgModule({
  declarations: [
    AppComponent,
    HotelsListComponent,
    OffersListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    HttpClientModule
  ],
    providers: [{ provide: API_BASE_URL, useValue: 'http://localhost:5000' }],
    bootstrap: [AppComponent]
})
export class AppModule { }
