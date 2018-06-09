import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";

@Component({
  selector: 'pr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  loginForm;
  hidePassword = true;

  subscriptionList: Subscription[] = [];

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email:'',
      password:''
    });
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  submitForm(){
    const formModel = this.loginForm.value;
    var user = {
      email: formModel.email,
      password: formModel.password
    };
    
    this._authService.loginHttp(user);
  }
}
