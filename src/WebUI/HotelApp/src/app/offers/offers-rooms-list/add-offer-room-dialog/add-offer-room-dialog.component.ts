import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoomDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-add-offer-room-dialog',
  templateUrl: './add-offer-room-dialog.component.html',
  styleUrls: ['./add-offer-room-dialog.component.scss']
})
export class AddOfferRoomDialogComponent implements OnInit {
  rooms: RoomDto[];
  selectedRoom: RoomDto | undefined;

  constructor(
      private dialogRef: MatDialogRef<AddOfferRoomDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: RoomDto[]) {

      this.rooms = data;
  }

  ngOnInit(): void {
    this.rooms = this.rooms;
  }

  save() {
      this.dialogRef.close(this.selectedRoom);
  }

  close() {
      this.dialogRef.close();
  }
}
