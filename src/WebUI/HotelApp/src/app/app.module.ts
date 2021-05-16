import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent, HOTEL_TOKEN } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { OffersListComponent } from './offers/offers-list/offers-list.component';
import { RoomsListComponent } from './rooms/rooms-list.component';
import { API_BASE_URL } from './web-api-client';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { OffersAddEditComponent } from './offers/offers-add-edit/offers-add-edit.component';
import { OfferRoomsListComponent } from './offers/offers-rooms-list/offers-rooms-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RoomsAddComponent } from './rooms/rooms-add/rooms-add.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AddOfferRoomDialogComponent } from './offers/offers-rooms-list/add-offer-room-dialog/add-offer-room-dialog.component';
import { ReservationsComponent } from './reservations/reservations.component';

@NgModule({
  declarations: [
    AppComponent,
    OffersListComponent,
    RoomsListComponent,
    RoomsAddComponent,
    OffersAddEditComponent,
    OfferRoomsListComponent,
    AddOfferRoomDialogComponent,
    ReservationsComponent
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
    MatDialogModule,
    HttpClientModule
  ],
    providers: [{ provide: API_BASE_URL, useValue: 'http://localhost:5000' },
     {provide: HOTEL_TOKEN, useValue: '$2a$11$M3hY1eNjsXD4PDEuoJGrSOJLLdvfBvTOo3M0SFurlni7GiQVoHMRS'}],
    bootstrap: [AppComponent]
})
export class AppModule { }
