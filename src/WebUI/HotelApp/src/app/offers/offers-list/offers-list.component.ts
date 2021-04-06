import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OfferClient, OfferDto } from '@app/web-api-client';

@Component({
  selector: 'app-offers-list',
  templateUrl: './offers-list.component.html',
  styleUrls: ['./offers-list.component.scss'],
})
export class OffersListComponent implements AfterViewInit {
  columnsToDisplay = ['offerId', 'title'];
  dataSource = new MatTableDataSource<OfferDto>();
  displayedPage: number = 1;
  pageSize: number = 5;
  isActive: boolean | null = null;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private offerClient: OfferClient) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    const offersRequest = this.offerClient.getOffersWithPagination(this.displayedPage, this.pageSize);
    offersRequest.subscribe({
      next: (value) => {
        console.log(value);
        this.dataSource = new MatTableDataSource<OfferDto>(value.items);
        this.dataSource.paginator = this.paginator;
      },
    });
  }
}
const offers: IOfferr[] = [
  { id: 11, name: 'Dr Nice' },
  { id: 12, name: 'Narco' },
  { id: 13, name: 'Bombasto' },
  { id: 14, name: 'Celeritas' },
  { id: 15, name: 'Magneta' },
  { id: 16, name: 'RubberMan' },
  { id: 17, name: 'Dynama' },
  { id: 18, name: 'Dr IQ' },
  { id: 19, name: 'Magma' },
  { id: 20, name: 'Tornado' },
];

interface IOfferr {
  id: number;
  name: string;
}
