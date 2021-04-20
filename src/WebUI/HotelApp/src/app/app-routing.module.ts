import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OffersAddComponent } from './offers/offers-add/offers-add.component';
import { OffersListComponent } from './offers/offers-list.component';
import { RoomsListComponent } from './offers/offers-rooms-list/offers-rooms-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/offers', pathMatch: 'full' },
  { path: 'offers', component: OffersListComponent },
  { path: 'offers/add', component: OffersAddComponent },
  { path: 'offers/:id/rooms', component: RoomsListComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
