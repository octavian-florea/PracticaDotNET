import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class StatisticsService {

  readonly rootURL = "http://localhost:64196/";

  constructor(private _http: HttpClient) { }

  getNrStudentsHttp(): Observable<Object>{
    return this._http.get(this.rootURL+'api/statistics/students');
  }

  getNrCompaniesHttp(): Observable<Object>{
    return this._http.get(this.rootURL+'api/statistics/companies');
  }

  getNrActiveActivitiesHttp(activityType:string): Observable<Object>{
    return this._http.get(this.rootURL+'api/statistics/activities/'+activityType);
  }

}
