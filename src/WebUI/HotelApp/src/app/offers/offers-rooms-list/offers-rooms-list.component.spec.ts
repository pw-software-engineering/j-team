import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomsListComponent } from './offers-rooms-list.component';

describe('OffersListComponent', () => {
  let component: RoomsListComponent;
  let fixture: ComponentFixture<RoomsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RoomsListComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
