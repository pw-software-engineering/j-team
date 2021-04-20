import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { OfferClient, RoomDto } from '../../web-api-client';

@Component({
  selector: 'app-offers-rooms-list',
  templateUrl: 'offers-rooms-list.component.html',
  styleUrls: ['offers-rooms-list.component.scss'],
})
export class RoomsListComponent implements AfterViewInit {
  columnsToDisplay = ['hotelRoomNumber'];
  myDataArray = new MatTableDataSource<RoomDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;
  offerId: string | null = "";

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(private offerClient: OfferClient, private route: ActivatedRoute) { }

  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.offerId = this.route.snapshot.paramMap.get('id');
    console.log(this.offerId);
    this.fetchData();
  }

  setData = (items: Array<RoomDto> | undefined) => {
    this.myDataArray.data = items ? items : [];
  }

  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.displayedPage = event.pageIndex;
    this.fetchData();
  }

  fetchData = () => {
    if (this.offerId == null) {
      this.setData([]);
      this.length = 0;
      return;
    }
    const roomsRequest = this.offerClient.rooms(+this.offerId, this.displayedPage + 1, this.pageSize);
    roomsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.totalCount!;
        this.setData(value.items);
      },
    });
  }
}
