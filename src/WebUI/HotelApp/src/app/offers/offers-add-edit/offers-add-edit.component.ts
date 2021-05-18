import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { HOTEL_TOKEN } from 'src/app/app.component';
import { CreateOfferCmd, OfferClient, UpdateOfferCmd } from 'src/app/web-api-client';

@Component({
  selector: 'app-offers-add-edit',
  templateUrl: './offers-add-edit.component.html',
  styleUrls: ['./offers-add-edit.component.scss']
})
export class OffersAddEditComponent implements OnInit {
  id!: number;
  isAddMode!: boolean;
  form!: FormGroup;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private offerClient: OfferClient,
      private route: ActivatedRoute,
      private router: Router,
      @Inject(HOTEL_TOKEN) private hotelToken:string
  ) {}

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    this.form = this.formBuilder.group({
        hotelId: [1],
        offerTitle: ['', Validators.required],
        costPerChild: ['', Validators.required],
        costPerAdult: ['', Validators.required],
        maxGuests: ['', [Validators.required]],
        description: [''],
        offerPreviewPicture: [''],
        pictures: [''],
        isActive: [true],
        isDeleted: [false]
    });

    if (!this.isAddMode) {
      this.offerClient.getOffer(this.id, this.hotelToken)
        .pipe(first())
        .subscribe(x => {
          this.form.patchValue(x);
        });
    }
  }

  get f() { return this.form.controls; }

  onSubmit() {
      this.submitted = true;

      if (this.form.invalid) {
          return;
      }

      if(this.isAddMode) {
        this.createOffer();
      }
      else {
        this.updateOffer();
      }

  }

  private createOffer() {
    const addRequest = this.offerClient.create(this.hotelToken, new CreateOfferCmd(this.form.value));
      addRequest.subscribe({
        next: (value) => {
          console.log(value);
          this.router.navigate(['../'], { relativeTo: this.route });
        }});
  }

  private updateOffer() {
    let cmd = new UpdateOfferCmd(this.form.value);
    const updateRequest = this.offerClient.update(this.id, this.hotelToken, cmd);
      updateRequest.subscribe({
        next: (value) => {
          console.log(value);
          this.router.navigate(['../../'], { relativeTo: this.route });
        }
      });
  }
}
