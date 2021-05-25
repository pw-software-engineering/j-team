import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { HOTEL_TOKEN } from 'src/app/app.component';
import { first } from 'rxjs/operators';
import { OfferClient, RoomClient, RoomDto } from '../../web-api-client';
import { AddOfferRoomDialogComponent } from './add-offer-room-dialog/add-offer-room-dialog.component';

@Component({
  selector: 'app-offers-rooms-list',
  templateUrl: 'offers-rooms-list.component.html',
  styleUrls: ['offers-rooms-list.component.scss'],
})
export class OfferRoomsListComponent implements AfterViewInit {
  columnsToDisplay = ['hotelRoomNumber', 'delete'];
  myDataArray = new MatTableDataSource<RoomDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;
  offerId: string | null = "";
  allRooms: RoomDto[] = [];

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(
      private offerClient: OfferClient,
      private route: ActivatedRoute,
      private dialog: MatDialog,
      private roomClient: RoomClient,
      @Inject(HOTEL_TOKEN) private hotelToken:string
     ) { }

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
    console.log("offer id " + (+this.offerId))
    const roomsRequest = this.offerClient.rooms(+this.offerId,null, this.displayedPage + 1, this.pageSize, this.hotelToken);
    roomsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.length!;
        this.setData(value);
      },
    });
    this.loadAllRooms();
  }

  deleteRoom(roomId: number): void {
    this.offerClient.deleteRoom(Number(this.offerId), roomId, this.hotelToken)
    .pipe(first())
        .subscribe(data => {
          console.log(data);
          this.fetchData();
        });
  }

  openAddDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;


    this.loadAllRooms();
    dialogConfig.data = this.allRooms;
    const dialogRef = this.dialog.open(AddOfferRoomDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data == null)
        return;
      this.offerClient.addRoom(Number(this.offerId), this.hotelToken, data)
        .pipe(first())
        .subscribe(data => {
          console.log(data);
          this.fetchData();
      });
    });
  }

  loadAllRooms(): void {
    this.roomClient.getRoomsWithPagination(null, 1, 100, this.hotelToken)
      .pipe(first())
      .subscribe(data => {
        this.allRooms = [];
        if(data) {
          data.forEach(x => {
            if(this.myDataArray.data.find(room => room.roomId == x.roomId) == undefined) {
              this.allRooms.push(x);
            }
          })
        }
    });
  }
}
