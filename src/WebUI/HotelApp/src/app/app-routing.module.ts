import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OffersListComponent } from './offers/offers-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/offers', pathMatch: 'full' },
  { path: 'offers', component: OffersListComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
