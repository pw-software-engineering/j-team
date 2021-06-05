import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { count } from 'rxjs/operators';
import { GetClientToken } from 'src/app/login/login.component';
import { HotelClient, HotelListedDto, HotelsClient, OfferClient, OfferDto } from '../../web-api-client';

@Component({
  selector: 'app-hotels-list',
  templateUrl: 'hotels-list.component.html',
  styleUrls: ['hotels-list.component.scss'],
})
export class HotelsListComponent implements AfterViewInit {
  columnsToDisplay = ['hotelName', 'country', 'city', 'offersButton'];
  dataSource = new MatTableDataSource<HotelListedDto>();
  displayedPage: number = 0;
  pageSize: number = 5;
  length: number = 0;

  country: string | null = null;
  hotelName: string | null = null;
  city: string | null = null;

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  constructor(
      private hotelClient: HotelsClient,
    ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.fetchData();
  }

  setCountry(value: string) {
    this.country = value;
    this.fetchData();
  }
  setHotelName(value: string) {
    this.hotelName = value;
    this.fetchData();
  }
  setCity(value: string) {
    this.city = value;
    this.fetchData();
  }

  setData = (items: Array<HotelListedDto> | undefined) => {
    this.dataSource.data = items ? items: [];
  }

  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.displayedPage = event.pageIndex;
    this.fetchData();
  }
  updateName(hotelNameFilter: string): void {
    this.hotelName = hotelNameFilter;
    this.fetchData();
  }
  updateCountry(countryFilter: string): void {
    this.country = countryFilter;
    this.fetchData();
  }
  updateCity(cityFilter: string): void {
    this.city = cityFilter;
    this.fetchData();
  }

  fetchData = () => {
    const hotelsRequest = this.hotelClient.getHotelsWithPagination(
      this.displayedPage + 1,
      this.pageSize,
      this.country,
      this.city,
      this.hotelName,
      GetClientToken());

    hotelsRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.length = value.length!;
        this.setData(value);
      },
    });
  }
}
