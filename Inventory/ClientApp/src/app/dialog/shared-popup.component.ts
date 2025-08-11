import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-hearing-popup',
  templateUrl: './shared-popup.html',
  imports: [
    MatDialogModule,
    MatButtonModule
  ]
})
export class SharedComponentDialog {
  title: string | undefined;
  body: string | undefined;

  constructor(
    public dialogRef: MatDialogRef<SharedComponentDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    this.title = this.data.title;
    this.body = this.data.body;
  }

  close(): void {
    this.dialogRef.close(false);
  }

  save(): void {
    this.dialogRef.close(true);
  }
}
