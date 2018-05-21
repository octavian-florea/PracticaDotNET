import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ActivityService{

    constructor(private _http: HttpClient){

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
                "activityName": "NTT Java cours",
                "activityType": "C",
                "activityDescription": "Junior Java 3 months course",
                "imageUrl": "assets/img/ntt.png",
                "location": "Cluj",
                "time":"acum 2 luni"
            }
        ]
    }

    getActivityHttp(){
        console.log('http');

        return this._http.get('http://localhost:64196/api/activity');
    }

    createActivityHttp(activity: any){
        console.log('save-http');

        return this._http.post('http://localhost:64196/api/activities', activity);
    }
}