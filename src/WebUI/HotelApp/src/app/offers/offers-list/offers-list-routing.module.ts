import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

import { Shell } from '@app/shell/shell.service';
import { OffersListComponent } from './offers-list.component';

const routes: Routes = [
  Shell.childRoutes([{ path: 'offers', component: OffersListComponent, data: { title: marker('About') } }]),
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class OffersListRoutingModule {}
