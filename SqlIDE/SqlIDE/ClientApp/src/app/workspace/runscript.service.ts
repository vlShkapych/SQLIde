
import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpHeaders} from '@angular/common/http';
import { Script } from './script.model';
import {DbResponse} from './dbResponse';
import {MatDialog} from '@angular/material/dialog';
import {DialogTableComponent} from '../tableDialog/tabledialog.component';

@Injectable()
export class RunScriptService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, public dialog: MatDialog) {}
  runScript(script: Script) {
    const options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
    this.http.post<DbResponse>(this.baseUrl + 'databasescript/run', JSON.stringify(script), options).subscribe(result => {
        console.log(result);
        this.openDialog(result);

    }, error => console.error(error));
  }

  openDialog(table: DbResponse) {
    this.dialog.open(DialogTableComponent, {
      data: table
    });
  }
}

