import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";
import { Observable } from 'rxjs/Observable';
import { startWith } from 'rxjs/operators/startWith';
import { map } from 'rxjs/operators/map';
import { ProfileService } from '../services/profile.service';
import { StudentProfile } from '../models/student-profile.model';

@Component({
  selector: 'pr-student-profile',
  templateUrl: './studentProfile.component.html',
  styleUrls: ['./studentProfile.component.css'],
  host: {'class': 'pr-full-width'}
})
export class StudentProfileComponent implements OnInit, OnDestroy {

  profileForm;

  subscriptionList: Subscription[] = [];

  filteredStates: Observable<any[]>;
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService, private _profileService:ProfileService) 
  { }

  ngOnInit() {
    this.profileForm = this.formBuilder.group({
      Name:"",
      //BirthDate: new Date(),
      //sex:"",
      Description:"",
      Telephone:"",
      City:"",
      Faculty:"",
      Specialization:"",
      StudyYear: "1"
    });

    this.subscriptionList.push(this._profileService.getStudentProfileHttp().subscribe(
      (res:StudentProfile) => { 
          this.profileForm.setValue({
            Name: res.Name,
            Description: res.Description,
            Faculty: res.FacultyId,
            Specialization: res.Specialization,
            StudyYear: res.StudyYear,
            Telephone: res.Telephone,
            City: res.City
          });
      },
      (err) => {}
    ));
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  submitForm(){
    const formModel = this.profileForm.value;
    var studentProfile: StudentProfile = {
      Name: formModel.Name,
      Description: formModel.Description,
      FacultyId: formModel.Faculty,
      Specialization: formModel.Specialization,
      StudyYear: formModel.StudyYear,
      Email: '',
      Telephone: formModel.Telephone,
      City: formModel.City
    };
    this._profileService.putStudentProfileHttp(studentProfile);

  }

  filter(val: string) {
    //return this.users
    //  .map(response => response.filter(option => { 
    //    return option.name.toLowerCase().indexOf(val.toLowerCase()) === 0
    //  }));
  }
}
