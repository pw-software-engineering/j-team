import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RoomClient, RoomDto } from '../web-api-client';

@Component({
  selector: 'app-room-list',
  templateUrl: 'room-list.component.html',
  styleUrls: ['room-list.component.scss'],
})
export class RoomListComponent implements AfterViewInit {
  columnsToDisplay = ['Id', 'RoomNo'];
  dataSource = new MatTableDataSource<RoomDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;
  roomfilter: string | null = null;

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(private roomClient: RoomClient) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.fetchData();
  }
  updatefilter(roomNumFilter: string): void {
    this.roomfilter = roomNumFilter;
    this.fetchData();
  }
 
  setData = (items: Array<RoomDto> | undefined) => {
    this.dataSource.data = items ? items: [];
  }

  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.displayedPage = event.pageIndex;
    this.fetchData();
  }

  fetchData = () => {
    const roomsRequest = this.roomClient.getRoomsWithPagination(this.displayedPage + 1, this.pageSize, this.roomfilter);
    roomsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.totalCount!;
        this.setData(value.items);
      },
    });
  }
}
