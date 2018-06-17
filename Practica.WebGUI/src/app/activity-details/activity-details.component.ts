import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Inject, OnDestroy, Injectable } from '@angular/core';
import { ActivityService } from '../services/activity.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-activity-details',
  templateUrl: './activity-details.component.html',
  styleUrls: ['./activity-details.component.css']
})
export class ActivityDetailsComponent implements OnInit, OnDestroy {
  id: string;
  companyName: string;
  activity: Activity;
  subscriptionList: Subscription[] = [];
  loggedIn: Boolean;

  constructor(private dialogRef: MatDialogRef<ActivityDetailsComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _activityService: ActivityService, private _authService:AuthService) {
    this.id = data.id;
    this.companyName = data.companyName;
    this.activity = this._activityService.DefaultActivity;
    this.subscriptionList.push(this._activityService.getActivityHttp(this.id).subscribe(
      (res:Activity) => { 
        this.activity = res;
      },
      (err) => { }
    ))
    this.subscriptionList.push(this._authService.isLoggedIn.subscribe((value) => {
      this.loggedIn = value
    }));
  }

  ngOnInit() {

  }

  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

}
