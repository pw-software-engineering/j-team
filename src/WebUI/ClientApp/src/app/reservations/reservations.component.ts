import {AfterViewInit, Component, OnInit} from '@angular/core';
import {ClientReservationResult, ClientReservationsClient} from "../web-api-client";
import {MatTableDataSource} from "@angular/material/table";
import {GetClientToken} from "../login/login.component";

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ClientReservationsComponent implements AfterViewInit {
  columnsToDisplay = ['Hotel', 'Rooom number', 'From', 'To', 'AdultNo', 'ChildrenNo', 'DeleteButton'];
  dataSource = new MatTableDataSource<ClientReservationResult>();
  constructor(private clientReservationsClient : ClientReservationsClient) {
  }
  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.fetchData();
  }
  fetchData = () => {
    const reservationsRequest = this.clientReservationsClient.getClientReservations(GetClientToken());
    reservationsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.setData(value);
      },
      error: (error) => {
        console.log(error);
        this.setData([]);
      }
    });
  }
  setData = (items: Array<ClientReservationResult> | undefined) => {
    this.dataSource.data = items ? items : [];
  }
  deleteReservation = (reservationId : number, hotelId : number, offerId : number) => {
    const reservationsRequest = this.clientReservationsClient.delete(reservationId, hotelId.toString(), offerId.toString(), GetClientToken());
    reservationsRequest.subscribe({
      next: (value) => {
        console.log(value);
      },
      error: (error) => {
        console.log(error);
        alert(error.response);
      }
    });
  }
}
