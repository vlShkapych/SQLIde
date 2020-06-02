import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Script} from '../workspace/script.model';
import {DbResponse} from '../workspace/dbResponse';
import {Observable} from 'rxjs';
import {Inject, Injectable} from '@angular/core';

@Injectable()
export class CheckconnService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}
  runScript(script: Script): Observable<DbResponse> {
    const options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
      return this.http.post<DbResponse>(this.baseUrl + 'databasescript/checkConnection', JSON.stringify(script), options);
  }
}
