import { ComponentFixture, TestBed } from '@angular/core/testing';
import { imports, providers } from '../app.module';
import { RoomsListComponent } from './rooms-list.component';


describe('OffersListComponent', () => {
  let component: RoomsListComponent;
  let fixture: ComponentFixture<RoomsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RoomsListComponent],
      imports: imports,
      providers: providers
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
