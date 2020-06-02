import {Component, Inject, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {DialogData} from './dialog-data';
import {CheckconnService} from './checkconn.service';
import {Script} from '../workspace/script.model';
import {DbResponse} from '../workspace/dbResponse';

@Component({
  selector: 'app-conndiag',
  templateUrl: 'connDiag.component.html',
  providers: [CheckconnService]
})
export class ConnectionDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ConnectionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private checkConnection: CheckconnService) {}
    onNoClick(): void {
    this.dialogRef.close();
  }
  onClick(): void {
    const script: Script = {
      DbType: 'msSql',
      DbScript: '',
      ConStr: this.data.connectionString,
      User: null
    };
    this.checkConnection.runScript(script).subscribe(result => {
      this.data.isConnected = result.Message === 'Connection is opened';
    }, error => {
      console.error(error);
    });

  }
}
