import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { CompanyProfile } from '../models/company-profile.model';
import { Router } from '@angular/router';

@Injectable()
export class ProfileService{

    readonly rootURL = "http://localhost:64196/";

    constructor(private _http: HttpClient,private router: Router){

    }

    getUniversitiesHttp(query:string){
        return this._http.get('http://localhost:64196/api/profiles/students/');
    }

    getCompanyProfileHttp(){
        return this._http.get(this.rootURL+'api/company/profile');
    }

    getTeacherProfileHttp(){
        return this._http.get('http://localhost:64196/api/profiles/techers');
    }

    postStudentProfileHttp(profile):void {
        const formData: FormData = new FormData();

        Object.keys(profile).forEach(key => {
            formData.append(key, profile[key]);
          });

        this._http.put('http://localhost:64196/api/profiles/students',formData).subscribe(
            (res:any) => { 
                this.router.navigate(['']);
            },
            (err) => {}
          );
    }

    postCompanyProfileHttp(companyProfile: CompanyProfile):void {
        const formData: FormData = new FormData();

        Object.keys(companyProfile).forEach(key => {
            formData.append(key, companyProfile[key]);
          });

        this._http.post(this.rootURL+'api/company/profile',formData).subscribe(
            (res:any) => { 
                this.router.navigate(['']);
            },
            (err) => {}
          );
    }

    postTeacherProfileHttp(profile):void {
        this._http.put('http://localhost:64196/api/profiles/techers/',{}).subscribe(
            (res:any) => { 
                this.router.navigate(['']);
            },
            (err) => {}
          );
    }
}