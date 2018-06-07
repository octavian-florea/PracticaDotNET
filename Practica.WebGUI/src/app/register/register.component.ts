import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";

@Component({
  selector: 'pr-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {

  registerForm;

  subscriptionList: Subscription[] = [];
  
  constructor(private formBuilder: FormBuilder, private _authService:AuthService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email:'taius1988@yahoo.com',
      password:'oFfspring1#',
      repassword:'oFfspring1#'
    });

    this.subscriptionList.push(this.registerForm.controls.email.valueChanges
          .subscribe(value=>{
            console.log(value);
          })
        )
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
      password: formModel.password
    };
    
    this._authService.loginHttp(user).subscribe(
      (res) => { console.log(res) },
      (err) => { console.log(err) }
    )
  }
}
