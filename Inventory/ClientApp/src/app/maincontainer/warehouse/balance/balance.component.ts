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
import { MatSort } from "@angular/material/sort";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from "@angular/material-moment-adapter";
import { GenericService } from "../../referencebooks/generic/services/generic.service";
import { Constants, MY_DATE_FORMATS } from "../../../constants";
import { BalanceService } from "./balance.service";
import { BalanceModel } from "./balancemodel";

@Component({
  selector: 'app-balance',
  templateUrl: 'balance.component.html',
  styleUrl: 'balance.component.scss',
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
    MatDatepickerModule
  ],
  providers: [
    BalanceService,
    GenericService
  ]
})

export class BalanceComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  public loading = true;
  datefrom: any;
  dateto: any;
  selectedresurces: number[] = [];
  selectedunits: number[] = [];

  resurces: any[] = [];
  units: any[] = [];

  displayedColumns: string[] = ['resource', 'unit', 'count'];

  container: BalanceModel[] = [];
  public ds = new MatTableDataSource<BalanceModel>();

  constructor(
    private service: BalanceService,
    private gservice: GenericService
  ) {

  }

  ngOnInit(): void {
    let today = new Date();
    let last = new Date();
    last.setMonth(today.getMonth() - 12);
    this.datefrom = last;
    this.dateto = today;
    this.gservice.getlist(Constants.resources_data_type_url).subscribe((resurces) => {
      this.resurces = resurces;
    });
    this.gservice.getlist(Constants.unitsofmeasurement_data_type_url).subscribe((units) => {
      this.units = units;
    });
    this.initDS();
  }

  initDS() {
    this.service.getlist(this.selectedresurces, this.selectedunits)
      .subscribe((response) => {
        this.container = response;
        this.ds.data = response;
        this.ds.paginator = this.paginator;
        this.loading = false;
      });
  }
}
