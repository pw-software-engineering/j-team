import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CreateReservationCmd, RoomClient, ReservationClient } from 'src/app/web-api-client';
import {HttpErrorResponse} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {catchError} from "rxjs/operators";

@Component({
  selector: 'app-make-reservation',
  templateUrl: './make-reservation.component.html',
  styleUrls: ['./make-reservation.component.scss']
})

export class MakeReservationComponent implements OnInit {

  handleError(error: HttpErrorResponse) {
    return throwError(error);
  }

  form!: FormGroup;
  submitted = false;
  hotelId: string | null = "";
  offerId: string | null = "";

  constructor(
    private formBuilder: FormBuilder,
    private reservationClient: ReservationClient,
    private roomClient: RoomClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.offerId = this.route.snapshot.paramMap.get('offerID');
    this.hotelId = this.route.snapshot.paramMap.get('hotelID');
    this.form = this.formBuilder.group({
      hotelId: [this.hotelId],
      offerId: [this.offerId],
      from: ['', Validators.required],
      to: ['', Validators.required],
      numberOfChildren: ['', Validators.required],
      numberOfAdults: ['', Validators.required]
    });
  }

  get f() { return this.form.controls; }

  getToken() {
    let token = localStorage.getItem('x-client-token');
    if (token == null)
      token = "";
    return token;
  }
  backToList(): void {
    this.router.navigate(['hotels', this.hotelId, 'offers']);
  }

  onSubmit(): Observable<any> {
    this.submitted = true;

    if (!this.form.invalid && this.offerId != null && this.hotelId != null) {
      let createReservationCmd = new CreateReservationCmd(this.form.value);
      createReservationCmd.from = new Date(this.form.getRawValue().from);
      createReservationCmd.to = new Date(this.form.getRawValue().to);

      const addRequest = this.reservationClient.create(+this.hotelId, +this.offerId, this.getToken(), createReservationCmd).pipe(catchError(this.handleError));

      addRequest.subscribe({
        next: (value) => {
          alert("Reservation has been made.");
          console.log(value);
        },
        error: (error) => {
          alert(error.response);
          console.log(error.response);
        }
      });
      return addRequest;
    }
    return new Observable();
  }
}

