import { ComponentFixture, TestBed } from '@angular/core/testing';
import { imports, providers } from 'src/app/app.module';

import { OfferRoomsListComponent } from './offers-rooms-list.component';

describe('OffersListComponent', () => {
  let component: OfferRoomsListComponent;
  let fixture: ComponentFixture<OfferRoomsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfferRoomsListComponent],
      imports: imports,
      providers: providers
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OfferRoomsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
