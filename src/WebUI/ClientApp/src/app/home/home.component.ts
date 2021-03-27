import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { HotelClient } from '../../../../web-api-client';

import { QuoteService } from './quote.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  quote: string | undefined;
  isLoading = false;

  constructor(private quoteService: QuoteService, private hotelClient: HotelClient) {}

  ngOnInit() {
    this.isLoading = true;
    //const response = this.hotelClient.getHotelsWithPagination(0, 1, 10);
    //console.log(response);
    this.quoteService
      .getRandomQuote({ category: 'dev' })
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe((quote: string) => {
        this.quote = quote;
      });
  }
}
