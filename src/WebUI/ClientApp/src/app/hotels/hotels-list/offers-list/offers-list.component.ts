import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { HotelClient, OfferClient, OfferDto } from '../../../web-api-client';

@Component({
  selector: 'app-offers-list',
  templateUrl: 'offers-list.component.html',
  styleUrls: ['offers-list.component.scss'],
})
export class OffersListComponent implements AfterViewInit {
  columnsToDisplay = ['title', 'costPerChild', 'costPerAdult', 'maxGuests'];
  dataSource = new MatTableDataSource<OfferDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;
  hotelId: number = 0;
  fromTime!: Date;
  toTime!: Date;
  minGuests!: number;
  costMin!: number;
  costMax!: number;

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(
      private hotelClient: HotelClient,
      private route: ActivatedRoute,
      private router: Router
    ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.hotelId = this.route.snapshot.params['id'];
    this.fetchData();
  }

  setData = (items: Array<OfferDto> | undefined) => {
    this.dataSource.data = items ? items: [];
  }

  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.displayedPage = event.pageIndex;
    this.fetchData();
  }

  backToList(): void {
    this.router.navigate(['hotels']);
  }

  fetchData = () => {
    this.hotelClient.getFilteredHotelOffersWithPagination(
      this.hotelId,
      this.hotelId,
      this.fromTime,
      this.toTime,
      this.minGuests,
      this.costMin,
      this.costMax,
      undefined
      )
      .pipe(first())
      .subscribe(data => {
        console.log(data);
        this.setData(data);
      });
  }
}
