import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule, DatePipe } from '@angular/common';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { ActivatedRoute, Data, RouterModule } from '@angular/router';
import { GetService } from '../services/getservice';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrls: ['./listing.component.scss'],
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    RouterModule,
    MatProgressBarModule,
    MatTableModule,
    MatMenuModule,
    MatPaginatorModule
  ],
  providers: [
    GetService,
    DatePipe
  ]
})
export class ListingComponent implements OnInit {
  public title: string | undefined;
  public mtype: string | undefined;
  public entryIDname: string = '';
  public loading = true;

  @ViewChild(MatSort) sort: MatSort | null = null;
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;

  displayedColumns: string[] = [];
  public dcolumns: string[] = [];

  dataSource = new MatTableDataSource<any>();

  constructor(
    private route: ActivatedRoute,
    public service: GetService,
    public dialog: MatDialog,
    public datepipe: DatePipe
  ) { }

  gettype(item: any) {
    return typeof (item);
  }

  updateModel() {
    this.loading = true;
    this.service.getlist(this.mtype)
      .subscribe((response) => {
        let array = Object.getOwnPropertyNames(response[0]);
        this.dcolumns = [];
        this.displayedColumns = [];
        this.displayedColumns.push('action');
        array.forEach((value, index) => {
          if (index !== 0) {
            this.dcolumns.push(value);
            this.displayedColumns.push(value);
          } else {
            this.entryIDname = value;
          }
        })
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.loading = false;
      });
  }

  ngOnInit() {
    this.route.data.subscribe((value: Data) => {
      this.title = value['title'];
      this.mtype = value['mtype'];
      this.updateModel();
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  scrollPageToTop(event: any) {
    setTimeout(function () {
      window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
      });
    }, 200);
  }
}
