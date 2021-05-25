import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HotelsListComponent } from './hotels/hotels-list/hotels-list.component';
import { OffersListComponent } from './hotels/hotels-list/hotel-offers-list/offers-list.component';
import {MakeReservationComponent} from "./hotels/hotels-list/hotel-offers-list/make-reservation/make-reservation.component";

const routes: Routes = [
  { path: 'hotels', component: HotelsListComponent },
  { path: 'hotels/:id/offers', component: OffersListComponent},
  { path: 'hotels/:hotelID/offers/:offerID/reservations', component: MakeReservationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
