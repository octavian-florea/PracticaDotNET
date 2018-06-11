import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";

@Component({
  selector: 'pr-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  host: {'class': 'pr-full-width'}
})
export class RegisterComponent implements OnInit, OnDestroy {

  registerForm;
  hidePassword = true;

  subscriptionList: Subscription[] = [];
  
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }

  constructor(private formBuilder: FormBuilder, private _authService:AuthService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email:'',
      password:'',
      role:'Student'
    });

    //this.subscriptionList.push(this.registerForm.controls.email.valueChanges
    //      .subscribe(value=>{
    //        console.log(value);
    //       // console.log(this._authService.getEmailExistsHttp(value));
    //      })
    //    )
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  submitForm(){
    const formModel = this.registerForm.value;
    var user = {
      email: formModel.email,
      password: formModel.password,
      role: formModel.role
    };  
    this._authService.registerHttp(user);
  }
}
