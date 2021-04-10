import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OfferClient, OfferDto } from '../web-api-client';

@Component({
  selector: 'app-offers-list',
  templateUrl: 'offers-list.component.html',
  styleUrls: ['offers-list.component.scss'],
})
export class OffersListComponent implements AfterViewInit {
  columnsToDisplay = ['title', 'isActive', 'costPerChild', 'costPerAdult', 'maxGuests'];
  dataSource = new MatTableDataSource<OfferDto>();
  displayedPage: number = 1;
  pageSize: number = 5;
  isActive: boolean | null = null;

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(private offerClient: OfferClient) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    const offersRequest = this.offerClient.getOffersWithPagination(this.displayedPage, this.pageSize);
    offersRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.dataSource = new MatTableDataSource<OfferDto>(value.items);
        this.dataSource.paginator = this.paginator;
      },
    });
  }
}
