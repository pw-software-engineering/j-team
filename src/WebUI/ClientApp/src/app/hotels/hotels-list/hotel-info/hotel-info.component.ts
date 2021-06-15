import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HotelsClient, HotelDto } from '../../../web-api-client';
import { GetClientToken } from 'src/app/login/login.component';

@Component({
  selector: 'app-hotel-info',
  templateUrl: './hotel-info.component.html',
  styleUrls: ['./hotel-info.component.scss']
})
export class HotelInfoComponent implements OnInit {


  hotelId: number = 0;
  Hotel: HotelDto = new HotelDto();
  constructor(
    private hotelClient: HotelsClient,
    private route: ActivatedRoute,
    private router: Router  ) { }

  ngOnInit(): void {
    this.hotelId = this.route.snapshot.params['id'] != null ? this.route.snapshot.params['id'] : this.hotelId;
    this.fetchData();
  }

  backToList(): void {
    this.router.navigate(['hotels']);
  }

  setData = (item: HotelDto ) => {
    this.Hotel = item;
  }

  fetchData = () => {
   

    this.hotelClient.getHotel(
      this.hotelId,
      GetClientToken()
    )
      .subscribe(data => {
        console.log(data);
        this.setData(data);
      });
  }

}
