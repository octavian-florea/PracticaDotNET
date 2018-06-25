import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import 'rxjs/add/operator/debounceTime';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";
import { Observable } from 'rxjs/Observable';
import { startWith } from 'rxjs/operators/startWith';
import { map } from 'rxjs/operators/map';
import { ProfileService } from '../services/profile.service';
import { StudentProfile } from '../models/student-profile.model';
import { AutocompleatService } from '../services/autocompleat.service';
import { Faculty } from '../models/faculty.model';
import { MatAutocompleteSelectedEvent } from '@angular/material';

@Component({
  selector: 'pr-student-profile',
  templateUrl: './studentProfile.component.html',
  styleUrls: ['./studentProfile.component.css'],
  host: {'class': 'pr-full-width'}
})
export class StudentProfileComponent implements OnInit, OnDestroy {

  profileForm;

  subscriptionList: Subscription[] = [];

  filteredFaculties: Observable<Faculty[]>;
  flagValidFaculty:boolean = true;
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService, private _profileService:ProfileService, private _autocompleatService:AutocompleatService) 
  { }

  ngOnInit() {
    this.profileForm = this.formBuilder.group({
      Name:"",
      //BirthDate: new Date(),
      //sex:"",
      Description:"",
      Telephone:"",
      City:"",
      FacultyId:"",
      Faculty:"",
      Specialization:"",
      StudyYear: "1"
    });

    this.subscriptionList.push(this._profileService.getStudentProfileHttp().subscribe(
      (res:StudentProfile) => { 
          this.profileForm.setValue({
            Name: res.Name,
            Description: res.Description,
            FacultyId: res.FacultyId,
            Faculty: res.FacultyName,
            Specialization: res.Specialization,
            StudyYear: res.StudyYear,
            Telephone: res.Telephone,
            City: res.City
          });
      },
      (err) => {}
    ));

    this.subscriptionList.push(this.profileForm.controls['Faculty'].valueChanges 
      .debounceTime(400)
      .subscribe(faculty => {
        let searchFaculty = faculty.toString().trim();
        if(searchFaculty != '' && !this.flagValidFaculty){
          this.filteredFaculties = this._autocompleatService.getFilteredFaculties(searchFaculty);
          }
        })
      )
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
      FacultyId: formModel.FacultyId,
      FacultyName: formModel.Faculty,
      Specialization: formModel.Specialization,
      StudyYear: formModel.StudyYear,
      Email: '',
      Telephone: formModel.Telephone,
      City: formModel.City
    };
    this._profileService.putStudentProfileHttp(studentProfile);

  }

  facultyOptionSelected(event:MatAutocompleteSelectedEvent){
    let faculty:Faculty = event.option.value;
    this.profileForm.controls['FacultyId'].setValue(faculty.Id);
    this.profileForm.controls['Faculty'].setValue(faculty.Name);
    this.flagValidFaculty = true;
  }
  
  onBlurFaculty(){
    if(!this.flagValidFaculty){
      this.profileForm.controls['FacultyId'].setValue('');
      this.profileForm.controls['Faculty'].setValue('');
    }
  }

  onKeyUpFaculty(){
    this.flagValidFaculty = false;
  }
  
}
