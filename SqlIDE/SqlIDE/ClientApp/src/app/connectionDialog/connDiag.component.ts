import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {DialogData} from './dialog-data';

@Component({
  selector: 'app-conndiag',
  templateUrl: 'connDiag.component.html',
})
export class ConnectionDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ConnectionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}
