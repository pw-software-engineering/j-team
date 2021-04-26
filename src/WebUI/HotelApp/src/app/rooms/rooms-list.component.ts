import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RoomClient, RoomDto } from '../web-api-client';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-rooms-list',
  templateUrl: 'rooms-list.component.html',
  styleUrls: ['rooms-list.component.scss'],
})
export class RoomsListComponent implements AfterViewInit {
  columnsToDisplay = ['Id', 'RoomNo', 'delButton'];
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
error: (error) => {
  console.log(error);
  this.setData([]);
      }
    });
  }
  DeleteRoom = (room: RoomDto) => {
      const delRequest = this.roomClient.delete(room.roomId!);
      delRequest.subscribe({
        next: (value) => {
          console.log(value?.status);
          this.fetchData();
        },
        error: (error) => {
          console.log(error);
          alert("Server error: could not remove room");
        }
      });
  }

}
