import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { OffersListComponent } from './offers/offers-list.component';
import { API_BASE_URL } from './web-api-client';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { OffersAddComponent } from './offers/offers-add/offers-add.component';
import { RoomsListComponent } from './offers/offers-rooms-list/offers-rooms-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    OffersListComponent,
    OffersAddComponent,
    RoomsListComponent
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
    NgbModule,
    HttpClientModule
  ],
    providers: [{ provide: API_BASE_URL, useValue: 'http://localhost:5000' }],
    bootstrap: [AppComponent]
})
export class AppModule { }
