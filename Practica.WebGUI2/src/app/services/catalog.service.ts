import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class CatalogService{

    constructor(private _http: HttpClient){

    }

   // gu(new Query())
   // var ob = {}
   // gu({attribute1: "asdas"})

    getUniversitiesHttp(query:string){
        return this._http.get('http://localhost:64196/api/universities/');
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