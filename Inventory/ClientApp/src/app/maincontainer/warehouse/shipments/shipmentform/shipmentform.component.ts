import { CommonModule, DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { ActivatedRoute, Router, RouterModule } from "@angular/router";
import { firstValueFrom } from "rxjs";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from "@angular/material-moment-adapter";
import { Constants, MY_DATE_FORMATS } from "../../../../constants";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { GenericService } from "../../../referencebooks/generic/services/generic.service";
import { MatSelectModule } from "@angular/material/select";
import { ShipmentEditService } from "./shipmentedit.service";
import { ShipmentEditItemModel, ShipmentEditModel } from "./shipmenteditmodel";
import { MatSliderModule } from "@angular/material/slider";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";

@Component({
  selector: 'app-shipmentform',
  templateUrl: './shipmentform.component.html',
  styleUrls: ['./shipmentform.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatSelectModule,
    RouterModule,
    MatSlideToggleModule
  ],
  providers: [
    ShipmentEditService,
    DatePipe,
    { provide: MAT_DATE_LOCALE, useValue: 'ru' },
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS] },
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS },
    GenericService
  ]
})
export class ShipmentFormComponent implements OnInit {
  myForm: FormGroup;
  id: number = 0;
  dataSource: MatTableDataSource<FormGroup>
  displayedColumns: string[] = ['action', 'resourceid', 'unitofmeasurementid', 'count'];

  showform = false;

  resurces: any[];
  units: any[];
  clients: any[];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private service: ShipmentEditService,
    private gservice: GenericService
  ) {

  }

  async ngOnInit() {
    Promise
      .all([
        await firstValueFrom(this.gservice.getlist(Constants.resources_data_type_url)),
        await firstValueFrom(this.gservice.getlist(Constants.unitsofmeasurement_data_type_url)),
        await firstValueFrom(this.gservice.getlist(Constants.clients_data_type_url))]
      )
      .then(([resurces, units, clients]) => {
        this.resurces = resurces;
        this.units = units;
        this.clients = clients;
      });
    let params = await firstValueFrom(this.route.params);
    this.id = +params['id'];
    if (this.id === 0) {
      this.fillForm(new ShipmentEditModel());
    } else {
      this.service
        .getitem(this.id)
        .subscribe((value) => {
          this.fillForm(value);
        });
    }
  }

  fillForm(value: ShipmentEditModel) {
    this.myForm = this.fb.group({
      id: value.id,
      number: [value.number, Validators.required],
      createdate: [value.createdate, Validators.required],
      clientid: [value.clientid, Validators.required],
      issign: [value.issign],
      items: this.fb.array([])
    });
    let tableRows = this.myForm.get('items') as FormArray;
    value.items.forEach((v) => {
      const newRow = this.fillrow(v);
      tableRows.push(newRow);
    });
    this.dataSource = new MatTableDataSource(tableRows.controls as FormGroup[]);
    this.showform = true;
  }

  fillrow(v: ShipmentEditItemModel): any {
    return this.fb.group({
      id: v.id,
      resourceid: [v.resourceid, Validators.required],
      unitofmeasurementid: [v.unitofmeasurementid, Validators.required],
      count: [v.count, [Validators.required, Validators.max(99999), Validators.min(1)]],
    });
  }

  addRow() {
    const newRow = this.fillrow(new ShipmentEditItemModel());
    let tableRows = this.myForm.get('items') as FormArray;
    tableRows.push(newRow);
    this.dataSource.data = tableRows.controls as FormGroup[];
  }

  deleteRow(index: number) {
    let tableRows = this.myForm.get('items') as FormArray;
    tableRows.removeAt(index);
    this.dataSource.data = tableRows.controls as FormGroup[];
  }

  onSubmit() {
    if (this.myForm.valid) {
      this.service.upsert(this.myForm.value).subscribe((v) => {
        this.router.navigate(['../'], { relativeTo: this.route });
      });
    }
  }

  checkbalance() {
    this.service.checkbalance(this.myForm.value).subscribe((check) => {
      if (check) {

      } else {
        this.myForm.controls['issign'].patchValue(false);
        alert('BALANCE not Valid');
      }
    });
  }
}
