import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Aplication } from '../models/aplication.model';
import { Router } from '@angular/router';
import { AplicationCreateDto } from '../models/aplication-create-dto.model';
import { CompanyAplicationTable } from '../models/company-aplication-table.model';
import * as moment from 'moment';

@Injectable()
export class AplicationService implements OnDestroy {

  readonly rootURL = "http://localhost:64196/";
  subscriptionList: Subscription[] = [];
  private readonly defaultAplication: CompanyAplicationTable = {
    Id : '',
    UserId : '',
    ActivityId: '',
    Description: '',
    FacultyId: '',
    Faculty: '',
    Specialization: '',
    StudyYear: '',
    StudentMessage: '',
    Status: '',
    CreatedDate: moment('1900-01-01'),
    ModifiedStateDate: moment('1900-01-01'),
    Name:'',
    City: '',
    Telephone: '',
    Email: '',
    ActivityTitle: '',
    ActivityType:''
  }
  get DefaultAplication():CompanyAplicationTable{
    return this.defaultAplication;
  }

  constructor(private _http: HttpClient,private router: Router) { }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  getAplications(id: string): Observable<Object>{
    return this._http.get(this.rootURL+'api/aplications/'+id);
  }

  getAplicationsByUserHttp(): Observable<Object>{
    return this._http.get(this.rootURL+'api/aplications/user');
  }

  getAplicationsByActivityHttp(id: string){
    return this._http.get(this.rootURL+'api/aplications/activities/'+id);
  }

  getAplicationsByActivityAndUserHttp(id: string){
    return this._http.get(this.rootURL+'api/aplications/activities/'+id+'/user/');
  }

  postAplicationHttp(aplication: AplicationCreateDto){
    return this._http.post(this.rootURL+'api/aplications', aplication);
  }

  putAplicationStatusHttp(id, status:number){
    return this._http.put(this.rootURL+'api/aplications/'+id, {Status: status});
  }

  deleteAplicationHttp(id: string){
    return this._http.delete(this.rootURL+'api/aplications/'+id);
  }
}
