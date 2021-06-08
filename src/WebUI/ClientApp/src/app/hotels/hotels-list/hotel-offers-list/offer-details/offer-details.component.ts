import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GetClientToken } from 'src/app/login/login.component';
import { DetailedOfferDto, HotelsClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.scss']
})
export class OfferDetailsComponent implements OnInit {

  hotelId!: number;
  offerId!: number;
  offer?: DetailedOfferDto = undefined;

  constructor(
    private hotelClient: HotelsClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.hotelId = this.route.snapshot.params['id'];
    this.offerId = this.route.snapshot.params['offerID'];

    this.getOfferInfo();
  }

  private getOfferInfo(): void {
    const detailsRequest = this.hotelClient.getOfferInfo(this.hotelId, this.offerId, GetClientToken());
    detailsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.offer = value;
      }
    })
  }

  public goBack(): void {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

}
