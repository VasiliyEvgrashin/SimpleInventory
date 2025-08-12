import { Component, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Data, Params, Router, RouterModule } from '@angular/router';
import { SharedComponentDialog } from '../../../../dialog/shared-popup.component';
import { CommonModule, DatePipe } from '@angular/common';
import { Definition } from '../definition';
import { GenericService } from '../services/generic.service';
import { GetmodelService } from '../services/getmodel.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { TitleCaseExtendedPipe } from '../titlepipe';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { firstValueFrom, forkJoin, lastValueFrom } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    TitleCaseExtendedPipe,
    MatDatepickerModule,
    MatCheckboxModule,
    RouterModule
  ],
  providers: [
    GenericService,
    GetmodelService,
    DatePipe
  ]
})
export class DetailComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  boomer: boolean = false;
  model: any;
  modelMeta: Definition[] = [];
  id: number = 0;
  mtype: string = '';
  odialog: MatDialogRef<SharedComponentDialog> | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: GenericService,
    public dialog: MatDialog,
    private resolver: GetmodelService
  ) { }

  async ngOnInit() {
    Promise
      .all([await firstValueFrom(this.route.data), await firstValueFrom(this.route.params)])
      .then(([data, params]) => {
        this.id = +params['id'];
        this.mtype = data['mtype'];
        if (this.id === 0) {
          this.model = this.resolver.getModel(this.mtype).model;
          this.modelMeta = this.model.getDefinition();
          let controls = this.model.getControls(this.service);
          this.form = new FormGroup(controls);
        } else {
          this.service
            .fetchOne(this.id, this.mtype)
            .subscribe((model) => {
              this.model = model;
              this.modelMeta = this.model.getDefinition();
              let controls = this.model.getControls(this.service);
              this.form = new FormGroup(controls);
            });
        }
      });
  }

  routetoback() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  onSubmit(): void {
    this.odialog = this.dialog.open(SharedComponentDialog, {
      width: '500px',
      data: {
        title: 'Save item',
        body: 'Are you sure that you want to save this item?',
      }
    });
    this.odialog.afterClosed().subscribe((data) => {
      if (data) {
        const formData = this.form.value;
        formData.id = this.id;
        if (this.id === 0) {
          this.service.post(formData, this.mtype).subscribe((response) => {
            this.routetoback();
          });
        } else {
          this.service
            .put(formData, this.mtype)
            .subscribe((response) => {
              this.routetoback();
            });
        }
      }
    });
  }
}
