import { CommonModule, DatePipe } from "@angular/common";
import { Component, OnInit, ViewChild } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatMenuModule } from "@angular/material/menu";
import { MatPaginator, MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatSelectModule } from "@angular/material/select";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { RouterModule } from "@angular/router";
import { TitleCaseExtendedPipe } from "../../referencebooks/generic/titlepipe";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { GenericService } from "../../referencebooks/generic/services/generic.service";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from "@angular/material-moment-adapter";
import { Constants, MY_DATE_FORMATS } from "../../../constants";
import { ShipmentsService } from "./shipments.service";
import { ShipmentListModel } from "./shipmentlistmodel";

@Component({
  selector: 'app-shipments',
  templateUrl: 'shipments.component.html',
  styleUrl: 'shipments.component.scss',
  imports: [
    CommonModule,
    FormsModule,
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
    MatPaginatorModule,
    TitleCaseExtendedPipe,
    MatDatepickerModule
  ],
  providers: [
    DatePipe,
    ShipmentsService,
    GenericService,
    { provide: MAT_DATE_LOCALE, useValue: 'ru' },
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS] },
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS },
  ]
})

export class ShipmentsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  public loading = true;
  datefrom: any;
  dateto: any;
  selectednumbers: string[] = [];
  selectednames: string[] = [];
  selectedunitnames: string[] = [];
  selectedclients: string[] = [];

  numbers: string[] = [];
  names: string[] = [];
  unitnames: string[] = [];
  clients: string[] = [];

  scolumns: string[] = ['number', 'client', 'issign', 'createdate', 'items'];
  displayedColumns: string[] = ['action', ...this.scolumns];

  container: ShipmentListModel[] = [];
  etalon: ShipmentListModel[] = [];
  public ds = new MatTableDataSource<ShipmentListModel>();

  constructor(
    private service: ShipmentsService,
    private gservice: GenericService,
    private dpipe: DatePipe
  ) {

  }

  ngOnInit(): void {
    let today = new Date();
    let last = new Date();
    last.setMonth(today.getMonth() - 12);
    this.datefrom = last;
    this.dateto = today;
    this.gservice.getlist(Constants.resources_data_type_url).subscribe((resurces) => {
      this.names = resurces.map((m: { name: string; }) => m.name);
    });
    this.gservice.getlist(Constants.unitsofmeasurement_data_type_url).subscribe((units) => {
      this.unitnames = units.map((m: { name: string; }) => m.name);
    });
    this.gservice.getlist(Constants.clients_data_type_url).subscribe((clients) => {
      this.clients = clients.map((m: { name: string; }) => m.name);
    });
    this.initDS();
  }

  initDS() {
    this.service.getlist(this.dpipe.transform(this.datefrom, 'yyyy-MM-dd'), this.dpipe.transform(this.dateto, 'yyyy-MM-dd'))
      .subscribe((response) => {
        this.container = response;
        this.etalon = JSON.parse(JSON.stringify(response));
        this.ds.data = response;
        this.ds.paginator = this.paginator;
        this.loading = false;
        this.numbers = response.map(m => m.number);
      });
  }

  filterItems() {
    this.container = JSON.parse(JSON.stringify(this.etalon));
    if (this.selectednumbers.length === 0 &&
      this.selectednames.length === 0 &&
      this.selectedunitnames.length === 0 &&
      this.selectedclients.length === 0)
    {
      this.ds.data = this.container;
    } else {
      this.ds.data = this.container.filter(f => {
        if (this.selectednames.length > 0) {
          f.items = f.items.filter(s => this.selectednames.indexOf(s.name) > -1);
          if (f.items.length === 0) {
            return false;
          }
        }
        if (this.selectedunitnames.length > 0) {
          f.items = f.items.filter(s => this.selectedunitnames.indexOf(s.uofm) > -1);
          if (f.items.length === 0) {
            return false;
          }
        }
        if (this.selectednumbers.length > 0) {
          return this.selectednumbers.indexOf(f.number) > -1
        }
        if (this.selectedclients.length > 0) {
          return this.selectedclients.indexOf(f.client) > -1
        }
        return true;
      });
    }
  }
}
