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
import {MakeReservationComponent} from "./hotels/hotels-list/hotel-offers-list/make-reservation/make-reservation.component";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { environment } from './../environments/environment';
import { ReviewsListComponent } from './reviews/reviews-list.component';
import { ReviewsAddEditComponent } from './reviews/review-add-edit/reviews-add-edit.component';
import { HotelInfoComponent } from './hotels/hotels-list/hotel-info/hotel-info.component';
import {LoginComponent} from "./login/login.component";
import {ClientReservationsComponent} from "./reservations/reservations.component";
import { OfferDetailsComponent } from './hotels/hotels-list/hotel-offers-list/offer-details/offer-details.component';


export const providers = [
  { provide: API_BASE_URL, useValue: environment.apiUrl }
]

@NgModule({
  declarations: [
    AppComponent,
    HotelsListComponent,
    OffersListComponent,
    MakeReservationComponent,
    ReviewsListComponent,
    ReviewsAddEditComponent,
    HotelInfoComponent,

    LoginComponent,
    ClientReservationsComponent,
    OfferDetailsComponent

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
    HttpClientModule,
  ],
    providers: providers,
    bootstrap: [AppComponent]
})
export class AppModule { }
