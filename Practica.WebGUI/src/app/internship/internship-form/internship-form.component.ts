import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { ActivityService } from "../../services/activity.service";
import { Activity } from '../../models/activity.model';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { ActivityDto } from '../../models/activity-dto.model';


@Component({
  selector: 'app-internship-form',
  templateUrl: './internship-form.component.html',
  styleUrls: ['./internship-form.component.css'],
  host: {'class': 'pr-full-width'}
})
export class InternshipFormComponent implements OnInit, OnDestroy {

  activityForm: FormGroup;
  id: string;
  flagNewActivity: boolean = false;
  activity: Activity;
  subscriptionList: Subscription[] = [];
  flagShowPublishButton: boolean = false;
  flagShowUnpublishButton: boolean = false;
  readonly defaultPublushDate = "9999-12-31";
  
  constructor(private _formBuilder: FormBuilder, private _activityService: ActivityService, private _route: ActivatedRoute) { 
    this._route.paramMap
      .subscribe(parms =>{
        this.id = parms.get("id");
        if(this.id == "new"){
          this.flagNewActivity = true;
          this.flagShowPublishButton = true;
        } 
      })
  }

  ngOnInit() {
    this.activityForm = this._formBuilder.group({
      Title: '',
      Type: 'practica',
      Description:'',
      StartDate: moment(new Date()),
      EndDate: moment(new Date(Date.now() + (30 * 24 * 60 * 60 * 1000))), // + 30 days 
      PublishDate: this.defaultPublushDate,
      ExpirationDate: moment(new Date(Date.now() + (90 * 24 * 60 * 60 * 1000))), // + 90 days
      Country:'',
      City:'',
      Address:''
    });

    if(!this.flagNewActivity){
      this.subscriptionList.push(this._activityService.getActivityHttp(this.id).subscribe(
        (res:Activity) => { 
            this.activityForm.setValue({
              Title: res.Title,
              Type: res.Type,
              Description: res.Description,
              StartDate: moment(res.StartDate),
              EndDate: moment(res.EndDate),
              PublishDate: moment(res.PublishDate).format('YYYY-MM-DD'),
              ExpirationDate: moment(res.ExpirationDate),
              Country: res.Country,
              City: res.City,
              Address: res.Address
            });

            //if PublishDate Active flip Publish/Unpublish button
            if(moment(res.PublishDate).format('YYYY-MM-DD') != this.defaultPublushDate){     
              this.flagShowPublishButton = false;
              this.flagShowUnpublishButton = true;
            }else{
              this.flagShowPublishButton = true;
              this.flagShowUnpublishButton = false;
            }
        },
        (err) => {}
      ));
    }
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  submitForm(){
    const formModel = this.activityForm.value;   
    var activityToCreate: ActivityDto = {
      Id: this.id,
      Title: formModel.Title,
      Description: formModel.Description,
      StartDate: formModel.StartDate.format('YYYY-MM-DD'),
      EndDate: formModel.EndDate.format('YYYY-MM-DD'),
      PublishDate: formModel.PublishDate,
      ExpirationDate: formModel.ExpirationDate.format('YYYY-MM-DD'),
      Type: formModel.Type,
      Country: formModel.Country,
      City: formModel.City,
      Address: formModel.Address
    };
    if(this.flagNewActivity)
      this._activityService.postActivityHttp(activityToCreate);
    else
      this._activityService.putActivityHttp(activityToCreate);
  }

  publish(){
    this.activityForm.controls['PublishDate'].setValue(moment(new Date()).format('YYYY-MM-DD'));
    this.submitForm()
  }

  unpublish(){
    this.activityForm.controls['PublishDate'].setValue(this.defaultPublushDate);
    this.submitForm()
  }

  delete(){
    this._activityService.deleteActivityHttp(this.id);
  }

}
