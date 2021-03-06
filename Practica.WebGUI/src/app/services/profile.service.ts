import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { CompanyProfile } from '../models/company-profile.model';
import { Router } from '@angular/router';
import { StudentProfile } from '../models/student-profile.model';
import 'rxjs/add/operator/take';

@Injectable()
export class ProfileService{

    readonly rootURL = "http://localhost:64196/";

    constructor(private _http: HttpClient,private router: Router){}


    // ============ COMPANY ZONE =========================================
    private readonly defaultCompanyProfile: CompanyProfile = {
        Logo: null,
        LogoExtension: null,
        Name: '',
        Description: '',
        Website: '',
        Adress: ''
    }
    get DefaultCompanyProfile():CompanyProfile{
        return this.defaultCompanyProfile;
    }

    getCompanyProfileHttp(){
        return this._http.get(this.rootURL+'api/company/profile').take(1);
    }

    getCompanyProfileHttpById(id:string){
        return this._http.get(this.rootURL+'api/company/profile/'+id).take(1);
    }

    putCompanyProfileHttp(companyProfile: CompanyProfile):void {
        const formData: FormData = new FormData();

        Object.keys(companyProfile).forEach(key => {
            formData.append(key, companyProfile[key]);
          });

        this._http.put(this.rootURL+'api/company/profile',formData).take(1).subscribe(
            (res:any) => { 
                this.router.navigate(['']);
            },
            (err) => {}
          );
    }


    // ============ STUDENT ZONE =========================================
    getStudentProfileHttp(){
        return this._http.get(this.rootURL+'api/student/profile').take(1);
    }

    putStudentProfileHttp(studentProfile: StudentProfile):void {
        this._http.put(this.rootURL+'api/student/profile',studentProfile).take(1).subscribe(
            (res:any) => { 
                this.router.navigate(['']);
            },
            (err) => {}
          );
    }


    // ============ TEACHER ZONE =========================================
    getTeacherProfileHttp(){
        return this._http.get('http://localhost:64196/api/profiles/techers').take(1);
    }

    putTeacherProfileHttp(profile):void {
        this._http.put('http://localhost:64196/api/profiles/techers/',{}).take(1).subscribe(
            (res:any) => { 
                this.router.navigate(['']);
            },
            (err) => {}
          );
    }
}