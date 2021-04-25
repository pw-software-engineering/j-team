import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OffersAddEditComponent } from './offers-add-edit.component';

describe('OffersAddComponent', () => {
  let component: OffersAddEditComponent;
  let fixture: ComponentFixture<OffersAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OffersAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OffersAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
