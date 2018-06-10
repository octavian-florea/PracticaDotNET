import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";

@Component({
  selector: 'pr-company-profile',
  templateUrl: './companyProfile.component.html',
  styleUrls: ['./companyProfile.component.css']
})
export class CompanyProfileComponent implements OnInit, OnDestroy {

  profileForm;

  subscriptionList: Subscription[] = [];
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService) { }

  ngOnInit() {
    this.profileForm = this.formBuilder.group({
      name:'',
      company:'',
      cf:'',
      phone:'',
      email:'',
      location:''
    });
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
}
