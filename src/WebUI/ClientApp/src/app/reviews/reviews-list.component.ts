import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { count } from 'rxjs/operators';
import { GetClientToken } from '../login/login.component';
import { HotelListedDto, ReviewDto, ReviewsClient } from '../web-api-client';

@Component({
  selector: 'app-reviews-list',
  templateUrl: 'reviews-list.component.html',
  styleUrls: ['reviews-list.component.scss'],
})
export class ReviewsListComponent implements AfterViewInit {
  columnsToDisplay = ['rating', 'content', 'reviewDate','editButton', 'deleteButton'];
  dataSource = new MatTableDataSource<ReviewDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;
  offerId = 0;
  hotelId = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(
      private reviewsClient: ReviewsClient,
      private route: ActivatedRoute,
      private router: Router
    ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    const hotelstr= this.route.snapshot.paramMap.get('id');
    const offerstr = this.route.snapshot.paramMap.get('offerID');
    this.hotelId = hotelstr ? +hotelstr :0;
    this.offerId = offerstr ? +offerstr : 0;
    this.fetchData();
  }


  setData = (items: Array<ReviewDto> | undefined) => {
    this.dataSource.data = items ? items: [];
  }

  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.displayedPage = event.pageIndex;
    this.fetchData();
  }
  Delete = (reviewId: number) => {
      const request = this.reviewsClient.deleteReview(this.hotelId, this.offerId, reviewId, GetClientToken());
      request.subscribe({
        next: (value) => {
          this.fetchData();
        }
      })
  }

  backToList(): void {
    this.router.navigate(['hotels', this.hotelId, 'offers']);
  }
  fetchData = () => {
    const hotelsRequest = this.reviewsClient.getReviewsWithPagination(
      this.hotelId,
      this.offerId,
      GetClientToken());

    hotelsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.length!;
        this.setData(value);
      },
      error: (error) => {
            alert("Server error: could not delete review.");
            console.log(error);
          }
    });
  }
}
