import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Inject, OnDestroy, Injectable } from '@angular/core';
import { ActivityService } from '../services/activity.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from '../services/auth.service';
import { AplicationService } from '../services/aplication.service';
import { Aplication } from '../models/aplication.model';
import { AplicationCreateDto } from '../models/aplication-create-dto.model';

@Component({
  selector: 'app-activity-details',
  templateUrl: './activity-details.component.html',
  styleUrls: ['./activity-details.component.css']
})
export class ActivityDetailsComponent implements OnInit, OnDestroy {
  id: string;
  companyName: string;
  activity: Activity;
  aplication: Aplication = null;
  subscriptionList: Subscription[] = [];
  loggedIn: Boolean;
  flagAlreadyApplied: boolean = false;

  constructor(private dialogRef: MatDialogRef<ActivityDetailsComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _activityService: ActivityService, private _authService:AuthService, private _aplication: AplicationService) {
    this.id = data.id;
    this.companyName = data.companyName;
    this.activity = this._activityService.DefaultActivity;

    this.subscriptionList.push(this._activityService.getActivityHttp(this.id).subscribe(
      (res:Activity) => { 
        this.activity = res;
      },
      (err) => { }
    ));

    this.subscriptionList.push(this._authService.isLoggedIn.subscribe((value) => {
      this.loggedIn = value
      if(this.loggedIn){
        this.subscriptionList.push(this._aplication.getAplicationsByActivityAndUserHttp(this.id).subscribe(
          (res:Aplication[]) => { 
            for(var elem of res){
              this.aplication = elem; 
              this.flagAlreadyApplied = true; 
            }           
          },
          (err) => { }
        ));
      }
    }));
  }

  ngOnInit() {

  }

  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  applyToActivity(){
    let aplication: AplicationCreateDto = {
      ActivityId: this.id,
      StudentMessage: ''
    }
    this.subscriptionList.push(this._aplication.postAplicationHttp(aplication).subscribe(
      (res:any) => { 
        this.dialogRef.close();
      },
      (err) => { }
    ));
  }

  deleteAplication(){
    if(this.aplication!=null){
      this.subscriptionList.push(this._aplication.deleteAplicationHttp(this.aplication.Id).subscribe(
          (res:any) => { 
            this.dialogRef.close();
          },
          (err) => { }
        )
      );
    }
  }

}
