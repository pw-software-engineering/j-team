import {Component, InjectionToken} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';
}

export let SESSION_TOKEN = new InjectionToken<string>('SESSION_TOKEN');
