import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Aplication } from '../models/aplication.model';
import { Router } from '@angular/router';
import { AplicationCreateDto } from '../models/aplication-create-dto.model';

@Injectable()
export class AplicationService implements OnDestroy {

  readonly rootURL = "http://localhost:64196/";
  subscriptionList: Subscription[] = [];

  constructor(private _http: HttpClient,private router: Router) { }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  getAplicationsByUserHttp(): Observable<Object>{
    return this._http.get(this.rootURL+'/api/aplications/user');
  }

  getAplicationsByActivityHttp(id: string){
    return this._http.get(this.rootURL+'/api/aplications/activities/'+id);
  }

  getAplicationsByActivityAndUserHttp(id: string){
    return this._http.get(this.rootURL+'/api/aplications/activities/'+id+'/user/');
  }

  postAplicationHttp(aplication: AplicationCreateDto){
    return this._http.post(this.rootURL+'/api/aplications', aplication);
  }

  deleteAplicationHttp(id: string){
    return this._http.delete(this.rootURL+'/api/aplications/'+id);
  }
}
