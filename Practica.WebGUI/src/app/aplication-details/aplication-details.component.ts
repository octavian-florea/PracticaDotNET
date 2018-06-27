import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Inject, OnDestroy, Injectable } from '@angular/core';
import { ActivityService } from '../services/activity.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from '../services/auth.service';
import { AplicationService } from '../services/aplication.service';
import { AplicationCreateDto } from '../models/aplication-create-dto.model';
import 'rxjs/add/operator/take';
import { CompanyAplicationTable } from '../models/company-aplication-table.model';
import { Status } from '../models/status.enum';

@Component({
  selector: 'app-aplication-details',
  templateUrl: './aplication-details.component.html',
  styleUrls: ['./aplication-details.component.css']
})
export class AplicationDetailsComponent implements OnInit, OnDestroy {
  id: string;
  aplication: CompanyAplicationTable = null;
  subscriptionList: Subscription[] = [];

  constructor(private dialogRef: MatDialogRef<AplicationDetailsComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _activityService: ActivityService, private _authService:AuthService, private _aplication: AplicationService) {
    this.id = data.id;
    this.aplication = _aplication.DefaultAplication;
  }

  ngOnInit() {
    this._aplication.getAplications(this.id).take(1).subscribe(
      (res:CompanyAplicationTable) => { 
        res.Status= Status[res.Status];
        this.aplication = res;                 
      },
      (err) => { }
    );
  }

  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  acceptStudent(){
    this.updateStatus(Status.Acceptat) ;  
  }

  declineStudent(){
    this.updateStatus(Status.Respins) ;  
  }
  updateStatus(status: number){
    this._aplication.putAplicationStatusHttp(this.id,status).take(1).subscribe(
      (res:any) => { 
        this.dialogRef.close();
      },
      (err) => { }
    );
  }

}
