import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OffersAddEditComponent } from './offers/offers-add-edit/offers-add-edit.component';
import { OffersListComponent } from './offers/offers-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/offers', pathMatch: 'full' },
  { path: 'offers', component: OffersListComponent },
  { path: 'offers/add', component: OffersAddEditComponent },
  { path: 'offers/edit/:id', component: OffersAddEditComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
