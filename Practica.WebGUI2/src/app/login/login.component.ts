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

  subscriptionList: Subscription[] = [];
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      userName:'hipius',
      password:'oFfspring1#'
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
      userName: formModel.userName,
      password: formModel.password
    };
    
    this._authService.loginHttp(user).subscribe(
      (res) => { console.log(res) },
      (err) => { console.log(err) }
    )
  }
}
