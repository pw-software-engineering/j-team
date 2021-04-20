import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { CreateOfferCmd, OfferClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-offers-add',
  templateUrl: './offers-add.component.html',
  styleUrls: ['./offers-add.component.scss']
})
export class OffersAddComponent implements OnInit {
  id!: number;
  isAddMode!: boolean;
  form!: FormGroup;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private offerClient: OfferClient,
      private route: ActivatedRoute,
      private router: Router
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
      // this.userService.getById(this.id)
      //     .pipe(first())
      //     .subscribe(x => this.form.patchValue(x));
      this.offerClient.getOffer(this.id)
        .pipe(first())
        .subscribe(x => this.form.patchValue(x));
  }
  }

  get f() { return this.form.controls; }

  onSubmit() {
      this.submitted = true;

      if (this.form.invalid) {
          return;
      }

      const addRequest = this.offerClient.create(new CreateOfferCmd(this.form.value));
      addRequest.subscribe({
        next: (value) => {
          console.log(value);
          this.router.navigate(['../'], { relativeTo: this.route });
        },
      });
  }
}
