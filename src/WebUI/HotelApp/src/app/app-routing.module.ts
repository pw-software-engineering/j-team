import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OffersAddEditComponent } from './offers/offers-add-edit/offers-add-edit.component';
import { OffersListComponent } from './offers/offers-list/offers-list.component';
import { RoomsListComponent } from './rooms/rooms-list.component';
import { OfferRoomsListComponent } from './offers/offers-rooms-list/offers-rooms-list.component';
import { RoomsAddComponent } from './rooms/rooms-add/rooms-add.component';
import { ReservationsComponent } from './reservations/reservations.component';

const routes: Routes = [
  { path: 'offers', component: OffersListComponent },

  { path: 'reservations', component: ReservationsComponent },
  { path: 'rooms', component: RoomsListComponent },
  { path: 'rooms/add', component: RoomsAddComponent },
    { path: 'offers/add', component: OffersAddEditComponent },
    { path: 'offers/edit/:id', component: OffersAddEditComponent },
  { path: 'offers/:id/rooms', component: OfferRoomsListComponent }

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
