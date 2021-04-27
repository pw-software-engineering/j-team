import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { HOTEL_TOKEN } from '../../app.component';
import { OfferClient, OfferDto } from '../../web-api-client';

@Component({
  selector: 'app-offers-list',
  templateUrl: 'offers-list.component.html',
  styleUrls: ['offers-list.component.scss'],
})
export class OffersListComponent implements AfterViewInit {
  columnsToDisplay = ['title', 'isActive', 'costPerChild', 'costPerAdult', 'maxGuests', 'delButton', 'roomsButton'];
  dataSource = new MatTableDataSource<OfferDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length:number = 0;
  showActive: boolean = true;
  showInactive: boolean = true;

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(
      private offerClient: OfferClient,
      @Inject(HOTEL_TOKEN) private hotelToken:string
    ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.fetchData();
  }
  DeleteOffer(id: number, isActive: boolean) {
    if (isActive) {
      alert("Offer must be deactiveted before being deleted");
      return;
    }
    const delRequest = this.offerClient.delete(id, this.hotelToken);
    delRequest.subscribe({
      next: (value) => {
        console.log(value?.status);
        this.fetchData();
      },
    });
  }
  setShowActive(value: boolean) {
    this.showActive = value;
    this.fetchData();
  }
  setShowInactive(value: boolean) {
    this.showInactive = value;
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

  fetchData = () => {

    if(!this.showInactive && !this.showActive){
      this.setData([]);
      this.length = 0;
      return;
    }

    const isActive = (this.showActive && this.showInactive) ? null: this.showActive;
    const offersRequest = this.offerClient.getOffersWithPagination(this.displayedPage + 1, this.pageSize, isActive, this.hotelToken);
    offersRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.totalCount!;
        this.setData(value.items);
      },
    });
  }
}
