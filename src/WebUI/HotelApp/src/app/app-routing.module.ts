import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OffersListComponent } from './offers/offers-list.component';
import { RoomListComponent } from './rooms/room-list.component';
const routes: Routes = [
  { path: 'offers', component: OffersListComponent },
  { path: 'rooms', component: RoomListComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
