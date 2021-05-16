import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { HOTEL_TOKEN } from '../app.component';
import { ReservationsClient, ReservationDto } from '../web-api-client';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements AfterViewInit {
  columnsToDisplay = ['Id', 'From', 'To', 'AdultNo', 'ChildrenNo','ClientName'];
  dataSource = new MatTableDataSource<ReservationDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;
  roomIdFilter: number | null = null;
  currentOnlyFilter: boolean | null = null;
  token: string = '';
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(private reservationsClient: ReservationsClient, @Inject(HOTEL_TOKEN) hotelToken: string) {
    this.token = hotelToken;
  }

  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.fetchData();
  }
  fetchData = () => {
    const reservationsRequest = this.reservationsClient.getReservationsWithPagination(this.displayedPage + 1, this.pageSize, this.roomIdFilter, this.currentOnlyFilter, this.token);
    reservationsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.totalCount!;
        this.setData(value.items);
      },
      error: (error) => {
        console.log(error);
        this.setData([]);
      }
    });
  }
  updatefilter(roomIdFilter: string): void {
    this.roomIdFilter = Number(roomIdFilter);
    this.fetchData();
  }
  setData = (items: Array<ReservationDto> | undefined) => {
    this.dataSource.data = items ? items : [];
  }
  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.displayedPage = event.pageIndex;
    this.fetchData();
  }
  setShowCurrentOnly(value: boolean) {
    this.currentOnlyFilter = value;
    this.fetchData();
  }
}
