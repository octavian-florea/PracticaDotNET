import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";
import { ErrorDialogComponent } from '../dialog/errorDialog.component';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'pr-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {

  registerForm;
  hidePassword = true;

  subscriptionList: Subscription[] = [];
  
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }

  constructor(private formBuilder: FormBuilder, private _authService:AuthService, public dialog : MatDialog, private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email:'',
      password:''
    });

    this.subscriptionList.push(this.registerForm.controls.email.valueChanges
          .subscribe(value=>{
            console.log(value);
           // console.log(this._authService.getEmailExistsHttp(value));
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
      password: formModel.password,
      role:'student'
    };
    
    this._authService.registerHttp(user).subscribe(
      (res:any) => { 
        console.log(res) 
        localStorage.setItem('userToken',res.token);
        this.router.navigate(['']);
      },
      (err) => { this.showError(err.error) }
    )
  }

  showError(error : string) : void {
    this.dialog.open(ErrorDialogComponent, {
      data: {errorMsg: error} ,width : '250px'
    });
  }
}
