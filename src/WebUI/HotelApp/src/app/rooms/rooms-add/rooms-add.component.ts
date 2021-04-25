import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CreateOfferCmd, CreateRoomCmd, OfferClient, RoomClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-rooms-add',
  templateUrl: './rooms-add.component.html',
  styleUrls: ['./rooms-add.component.scss']
})
export class RoomsAddComponent implements OnInit {

    form!: FormGroup;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private roomClient: RoomClient,
        private route: ActivatedRoute,
        private router: Router
    ) {}

    ngOnInit() {
        this.form = this.formBuilder.group({
            hotelId: [1],
            hotelRoomNumber: ['', Validators.required],
        });
    }

    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.form.invalid) {
            return;
        }

        const addRequest = this.roomClient.create(new CreateRoomCmd(this.form.value));
        addRequest.subscribe({
          next: (value) => {
            console.log(value);
            this.router.navigate(['../'], { relativeTo: this.route });
          },
          error: (error) => {
            alert("Server error: could not add room.");
            console.log(error);
            this.router.navigate(['../'], { relativeTo: this.route });
          }
        });
    }
}
