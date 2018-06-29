import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Activity } from '../models/activity.model';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { ActivityDto } from '../models/activity-dto.model';
import { Search } from '../models/search.model';
import * as moment from 'moment';
import 'rxjs/add/operator/take';

@Injectable()
export class ActivityService{

    readonly rootURL = "http://localhost:64196";
    private readonly defaultActivity: Activity = {
        Id : '',
        Title : '',
        Description: '',
        Type: '',
        StartDate: moment('1900-01-01'),
        EndDate: moment('1900-01-01'),
        PublishDate: moment('9999-12-31'),
        ExpirationDate: moment('1900-01-01'),
        Country:'',
        City:'',
        Address: '',
    }
    get DefaultActivity():Activity{
        return this.defaultActivity;
    }

    private search = new BehaviorSubject<Search>({SearchKey:'', City:''});

    get searchObserver(): Observable<Search> {
        return this.search.asObservable();
    }
    
    constructor(private _http: HttpClient,private router: Router){

    }

    getActivity(){
        return [
            {
                "activityId": 1,
                "activityName": "Iquest locuri de practica",
                "activityType": "LP",
                "activityDescription": "Locuri de practica pe timpul verii",
                "imageUrl": "assets/img/iquest.png",
                "location": "Brasov",
                "time":"acum 4 ore"
            },
            {
                "activityId": 2,
                "activityName": "PentaBar event",
                "activityType": "E",
                "activityDescription": "Meetup Microsoft Azure",
                "imageUrl": "assets/img/pentalog.png",
                "location": "Bucuresti",
                "time":"acum 5 zile"
            },
            {
                "activityId": 3,
                "activityName": "NTT Java cours 3",
                "activityType": "C",
                "activityDescription": "Junior Java 3 months course",
                "imageUrl": "assets/img/ntt.png",
                "location": "Cluj",
                "time":"acum 2 luni"
            },
            {
                "activityId": 4,
                "activityName": "NTT Java cours 4",
                "activityType": "C",
                "activityDescription": "Junior Java 3 months course",
                "imageUrl": "assets/img/ntt.png",
                "location": "Cluj",
                "time":"acum 2 luni"
            },
            {
                "activityId": 5,
                "activityName": "NTT Java cours 5",
                "activityType": "C",
                "activityDescription": "Junior Java 3 months course",
                "imageUrl": "assets/img/ntt.png",
                "location": "Cluj",
                "time":"acum 2 luni"
            }
            ,
            {
                "activityId": 6,
                "activityName": "NTT Java cours 6",
                "activityType": "C",
                "activityDescription": "Junior Java 3 months course",
                "imageUrl": "assets/img/ntt.png",
                "location": "Cluj",
                "time":"acum 2 luni"
            }
            ,
            {
                "activityId": 7,
                "activityName": "NTT Java cours 7",
                "activityType": "C",
                "activityDescription": "Junior Java 3 months course",
                "imageUrl": "assets/img/ntt.png",
                "location": "Cluj",
                "time":"acum 2 luni"
            }
        ]
    }

    getActivityHttp(id: string): Observable<Object>{
        return this._http.get(this.rootURL+'/api/activities/'+id).take(1);
    }

    getActivitiesHttp(searchObj: Search): Observable<Object>{
        let data = {};
        for (var key in searchObj) {
            data[key] = searchObj[key];
        }
        return this._http.get(this.rootURL+'/api/activities',{params: data}).take(1);
    }

    getActivitiesByUserHttp(): Observable<Object>{
        return this._http.get(this.rootURL+'/api/activities/user').take(1);
    }

    postActivityHttp(activity: ActivityDto): void{
        this._http.post(this.rootURL+'/api/activities', activity).take(1).subscribe(
            (res:any) => { 
                this.router.navigate(['/activities-company']);
            },
            (err) => { }
          );
    }

    putActivityHttp(activity: ActivityDto): void{
        this._http.put(this.rootURL+'/api/activities/'+activity.Id, activity).take(1).subscribe(
            (res:any) => { 
                this.router.navigate(['/activities-company']);
            },
            (err) => { }
          );
    }

    deleteActivityHttp(id: string): void{
        this._http.delete(this.rootURL+'/api/activities/'+id).take(1).subscribe(
            (res:any) => { 
                this.router.navigate(['/activities-company']);
            },
            (err) => { }
          );
    }

    setSearch(searchObj: Search) {
        this.search.next(searchObj);
    }
}