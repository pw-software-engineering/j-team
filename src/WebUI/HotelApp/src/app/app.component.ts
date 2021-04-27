import { Component, InjectionToken } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'HotelApp';
}
export const HOTEL_TOKEN = new InjectionToken<string>('HOTEL_TOKEN');
