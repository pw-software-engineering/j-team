import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HotelsListComponent } from './hotels/hotels-list/hotels-list.component';
import { OffersListComponent } from './hotels/hotels-list/hotel-offers-list/offers-list.component';
import {MakeReservationComponent} from "./hotels/hotels-list/hotel-offers-list/make-reservation/make-reservation.component";
import { ReviewsListComponent } from './reviews/reviews-list.component';
import { ReviewsAddEditComponent } from './reviews/review-add-edit/reviews-add-edit.component';
import { HotelInfoComponent } from './hotels/hotels-list/hotel-info/hotel-info.component';
import { LoginComponent } from './login/login.component';
import {ClientReservationsComponent} from "./reservations/reservations.component";
import { OfferDetailsComponent } from './hotels/hotels-list/hotel-offers-list/offer-details/offer-details.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { path: 'hotels', component: HotelsListComponent },
  { path: 'hotels/:id/offers', component: OffersListComponent},
  { path: 'hotels/:id/offers/:offerID', component: OfferDetailsComponent},
  { path: 'hotels/:id/offers/:offerID/reviews', component: ReviewsListComponent},
  { path: 'hotels/:id/offers/:offerID/reviews/add', component: ReviewsAddEditComponent},
  { path: 'hotels/:id/offers/:offerID/reviews/:reviewID', component: ReviewsAddEditComponent},
  { path: 'hotels/:id', component: HotelInfoComponent },
  { path: 'hotels/:hotelID/offers/:offerID/reservations', component: MakeReservationComponent},
  { path: 'reservations', component: ClientReservationsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
