import { AfterViewInit, ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { HotelClient, OfferDto } from '../../../web-api-client';

@Component({
  selector: 'app-offers-list',
  templateUrl: 'offers-list.component.html',
  styleUrls: ['offers-list.component.scss'],
})
export class OffersListComponent implements AfterViewInit {
  columnsToDisplay = ['title', 'costPerChild', 'costPerAdult', 'maxGuests', 'reservationButton', 'reviewsButton'];
  dataSource = new MatTableDataSource<OfferDto>();
  hotelId: number = 0;
  fromTime: Date | null = null;
  toTime: Date | null = null;
  minGuests: number | null = null;
  costMin: number | null = null
  costMax: number | null = null

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(
      private hotelClient: HotelClient,
      private route: ActivatedRoute,
      private router: Router
    ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.hotelId = this.route.snapshot.params['id'] != null ? this.route.snapshot.params['id'] : this.hotelId;
    this.fetchData();
  }

  setData = (items: Array<OfferDto> | undefined) => {
    this.dataSource.data = items ? items: [];
  }

  backToList(): void {
    this.router.navigate(['hotels']);
  }

  refresh(): void {
    this.fromTime = null;
    this.toTime = null;
    this.minGuests = null;
    this.costMax = null;
    this.costMin = null;
    this.fetchData();
  }

  fetchData = () => {
    let fromTime = null;
    if (this.fromTime != null)
      fromTime = new Date(this.fromTime);

    let toTime = null;
    if (this.toTime != null)
      toTime = new Date(this.toTime);

    this.hotelClient.getFilteredHotelOffersWithPagination(
      this.hotelId,
      this.hotelId,
      fromTime,
      toTime,
      this.minGuests,
      this.costMin,
      this.costMax,
      ""
      )
      .pipe(first())
      .subscribe(data => {
        console.log(data);
        this.setData(data);
      });
  }
}
