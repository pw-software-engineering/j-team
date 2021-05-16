import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HotelsListComponent } from './hotels/hotels-list/hotels-list.component';
import { OffersListComponent } from './hotels/hotels-list/hotel-offers-list/offers-list.component';

const routes: Routes = [
  { path: 'hotels', component: HotelsListComponent },
  { path: 'hotels/:id/offers', component: OffersListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
