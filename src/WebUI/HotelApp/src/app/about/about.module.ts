import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

import { AboutRoutingModule } from './about-routing.module';
import { AboutComponent } from './about.component';
import { OffersListRoutingModule } from '@app/offers/offers-list/offers-list-routing.module';

@NgModule({
  imports: [CommonModule, TranslateModule, AboutRoutingModule, OffersListRoutingModule],
  declarations: [AboutComponent],
})
export class AboutModule {}
