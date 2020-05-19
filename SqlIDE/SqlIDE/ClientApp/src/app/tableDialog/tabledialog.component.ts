import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {DialogData} from '../connectionDialog/dialog-data';
import {DbResponse} from '../workspace/dbResponse';

@Component({
  selector: 'app-dialog-table',
  templateUrl: 'tabledialog.component.html',
  styles: [`
    th, td {
      padding: 15px;
      text-align: center;
    }
  `]
})
export class DialogTableComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) private data: DbResponse) {
  }
  ngOnInit(): void {
    this.data.Table = `<table>${this.data.Table}</table>`;
  }
}
