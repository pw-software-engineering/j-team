import { ComponentFixture, TestBed } from '@angular/core/testing';
import { imports, providers } from 'src/app/app.module';

import { OffersListComponent } from './offers-list.component';

describe('OffersListComponent', () => {
  let component: OffersListComponent;
  let fixture: ComponentFixture<OffersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OffersListComponent],
      imports: imports,
      providers: providers
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OffersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
