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
  columnsToDisplay = ['Hotel', 'Offer', 'From', 'To', 'AdultNo', 'ChildrenNo', 'DeleteButton'];
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
        alert(error.response);
        console.log(error);
        this.setData([]);
      }
    });
  }
  setData = (items: Array<ClientReservationResult> | undefined) => {
    this.dataSource.data = items ? items : [];
  }
  deleteReservation = (reservationId : number) => {
    //TODO: prawdziwe wartości offer id i hotel id
    const reservationsRequest = this.clientReservationsClient.delete(reservationId, "","", GetClientToken());
    reservationsRequest.subscribe({
      next: (value) => {
        console.log(value);
        alert("Reservation has been cancelled");
      },
      error: (error) => {
        console.log(error);
        alert(error.response);
      }
    });
  }
}
