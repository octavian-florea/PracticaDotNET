import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class ProfileService{

    constructor(private _http: HttpClient){

    }

    getUniversitiesHttp(query:string){
        return this._http.get('http://localhost:64196/api/profiles/students/');
    }

    getCompanyProfileHttp(user){
        return this._http.get('http://localhost:64196/api/profiles/companies/'+user.id);
    }

    getTeacherProfileHttp(user){
        return this._http.get('http://localhost:64196/api/profiles/techers/'+user.id);
    }

    updateStudentProfileHttp(user,profile){
        return this._http.put('http://localhost:64196/api/profiles/students/'+user.id,profile);
    }

    updateCompanyProfileHttp(user,profile){
        return this._http.put('http://localhost:64196/api/profiles/companies/'+user.id,profile);
    }

    updateTeacherProfileHttp(user,profile){
        return this._http.put('http://localhost:64196/api/profiles/techers/'+user.id,profile);
    }
}