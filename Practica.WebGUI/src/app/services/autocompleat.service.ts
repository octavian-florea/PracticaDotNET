import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { Faculty } from '../models/faculty.model';
import 'rxjs/add/operator/take';

@Injectable()
export class AutocompleatService{

    readonly rootURL = "http://localhost:64196/";

    constructor(private _http: HttpClient){}

    getFilteredFaculties(facultyName){
        return this._http.get(this.rootURL+'api/faculties/'+facultyName).map(response =>
        {
            return <Faculty[]>response;
        }).take(1);
    }
}