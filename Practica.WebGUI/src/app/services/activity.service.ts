import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Activity } from '../models/activity.model';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class ActivityService{

    readonly rootURL = "http://localhost:64196";

    constructor(private _http: HttpClient,private router: Router){

    }

    get(id:string){
        return {
            "activityId": 1,
            "activityName": "Iquest locuri de practica",
            "activityType": "LP",
            "activityDescription": "Locuri de practica pe timpul verii",
            "imageUrl": "assets/img/iquest.png",
            "location": "Brasov",
            "time":"acum 4 ore"
        }
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
        return this._http.get(this.rootURL+'/api/activities/'+id);
    }

    getActivitiesHttp(): Observable<Object>{
        return this._http.get(this.rootURL+'/api/activities');
    }

    getActivitiesByUserHttp(): Observable<Object>{
        return this._http.get(this.rootURL+'/api/activities/user');
    }

    postActivityHttp(activity: Activity): void{
        this._http.post(this.rootURL+'/api/activities', activity).subscribe(
            (res:any) => { 
                this.router.navigate(['/activities-company']);
            },
            (err) => { }
          );
    }

    putActivityHttp(activity: Activity): void{
        this._http.put(this.rootURL+'/api/activities/'+activity.Id, activity).subscribe(
            (res:any) => { 
                this.router.navigate(['/activities-company']);
            },
            (err) => { }
          );
    }
}