import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CreateOfferCmd, CreateReviewCmd, OfferClient, ReviewsClient, RoomClient, UpdateReviewCmd } from 'src/app/web-api-client';

@Component({
  selector: 'app-reviews-add-edit',
  templateUrl: './reviews-add-edit.component.html',
  styleUrls: ['./reviews-add-edit.component.scss']
})
export class ReviewsAddEditComponent implements OnInit {

    form!: FormGroup;
    submitted = false;
    hotelId = 0;
    offerId = 0;
    reviewId = 0;


    constructor(
        private formBuilder: FormBuilder,
        private reviewsClient: ReviewsClient,
        private route: ActivatedRoute,
        private router: Router,
    ) {}

    ngOnInit() {
        const hotelstr= this.route.snapshot.paramMap.get('id');
        const offerstr = this.route.snapshot.paramMap.get('offerID');
        const reviewstr = this.route.snapshot.paramMap.get('reviewID');
        this.hotelId = hotelstr? +hotelstr :0;
        this.offerId = offerstr? +offerstr :0;
        this.reviewId = reviewstr? +reviewstr :0;

        this.form = this.formBuilder.group({
            content: ['', Validators.required],
            rating: [0, [Validators.required, Validators.min(1), Validators.max(5)]],
        });

        if (this.isEdit()) {
          console.log("fetching review " + this.reviewId);
          this.reviewsClient.getReview(this.hotelId, this.offerId, this.reviewId, " ")
            .subscribe(x => {
              console.log(x)
              this.form.patchValue(x);
            });
        }
    }

    isEdit = () => this.reviewId >0

    backToList(): void {
      this.router.navigate(['hotels', this.hotelId, 'offers',this.offerId,'reviews']);
    }

    get f() { return this.form.controls; }

    createReview = () => {
        const cmd = new CreateReviewCmd();
        cmd.content = this.form.value.content;
        cmd.rating = this.form.value.rating;
        const addRequest = this.reviewsClient.createReview(this.hotelId,this.offerId, "", cmd);
        addRequest.subscribe({
          next: (value) => {
            console.log(value);
            this.router.navigate(['../'], { relativeTo: this.route });
          },
          error: (error) => {
            alert("Server error: could not add review.");
            console.log(error);
            this.router.navigate(['../'], { relativeTo: this.route });
          }
        });
    }
    editReview = () => {
        const cmd = new UpdateReviewCmd();
        cmd.content = this.form.value.content;
        cmd.rating = this.form.value.rating;
        const addRequest = this.reviewsClient.updateReview(this.hotelId,this.offerId,this.reviewId, "", cmd);
        addRequest.subscribe({
          next: (value) => {
            console.log(value);
            this.router.navigate(['../'], { relativeTo: this.route });
          },
          error: (error) => {
            alert("Server error: could not add review.");
            console.log(error);
            this.router.navigate(['../'], { relativeTo: this.route });
          }
        });
    }

    onSubmit() {
        this.submitted = true;

        if (this.form.invalid) {
            return;
        }
        if(this.isEdit()){
          this.editReview();
        }else{
          this.createReview();
        }

    }
}
