import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOfferRoomDialogComponent } from './add-offer-room-dialog.component';

describe('AddOfferRoomDialogComponent', () => {
  let component: AddOfferRoomDialogComponent;
  let fixture: ComponentFixture<AddOfferRoomDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddOfferRoomDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOfferRoomDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  //it('should create', () => {
    //expect(component).toBeTruthy();
  //});
});
