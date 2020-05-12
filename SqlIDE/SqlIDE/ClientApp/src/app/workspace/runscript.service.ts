
import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpHeaders} from '@angular/common/http';
import { Script } from './script.model';

@Injectable()
export class RunScriptService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}
  runScript(script: Script) {
    const options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
    this.http.post<string>(this.baseUrl + 'databasescript/run', JSON.stringify(script), options).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }
}
