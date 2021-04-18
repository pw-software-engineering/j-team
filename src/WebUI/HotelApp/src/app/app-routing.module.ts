import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OffersAddComponent } from './offers/offers-add/offers-add.component';
import { OffersListComponent } from './offers/offers-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/offers', pathMatch: 'full' },
  { path: 'offers', component: OffersListComponent },
  { path: 'offers/add', component: OffersAddComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
