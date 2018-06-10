import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";
import { Observable } from 'rxjs/Observable';
import { startWith } from 'rxjs/operators/startWith';
import { map } from 'rxjs/operators/map';
import { CatalogService } from '../services/catalog.service';

@Component({
  selector: 'pr-student-profile',
  templateUrl: './studentProfile.component.html',
  styleUrls: ['./studentProfile.component.css']
})
export class StudentProfileComponent implements OnInit, OnDestroy {

  profileForm;

  subscriptionList: Subscription[] = [];

  filteredStates: Observable<any[]>;
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService, private _catalogService:CatalogService) 
  { }

  ngOnInit() {
    this.profileForm = this.formBuilder.group({
      name:'',
      birthDate: new Date(),
      sex:'',
      email:'',
      phone:'',
      location:'',
      university:'Transilvania',
      faculty:'Matematica si informatica',
      specialisation:'Informatica',
      admissionYear:'2017'
    });

    //this.filteredStates = this.profileForm.controls.university.valueChanges
   // .pipe(
    //  startWith(''),
    //  map(university => university ? this.filterUniversity(university) : this.university.slice())
    //)
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  submitForm(){
    const formModel = this.profileForm.value;
    var user = {
      userName: formModel.userName,
      password: formModel.password
    };
    

  }

  filter(val: string) {
    //return this.users
    //  .map(response => response.filter(option => { 
    //    return option.name.toLowerCase().indexOf(val.toLowerCase()) === 0
    //  }));
  }
}
