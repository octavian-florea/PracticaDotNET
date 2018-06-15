import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { ActivityService } from "../../services/activity.service";
import { Activity } from '../../models/activity.model';
import { ActivatedRoute } from '@angular/router';


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
  
  constructor(private _formBuilder: FormBuilder, private _activityService: ActivityService, private _route: ActivatedRoute) { 
    this._route.paramMap
      .subscribe(parms =>{
        this.id = parms.get("id");
        if(this.id == "new") this.flagNewActivity = true;
      })
  }

  ngOnInit() {
    this.activityForm = this._formBuilder.group({
      Title: '',
      Type: 'practica',
      Description:'',
      StartDate: new Date(),
      EndDate: new Date(Date.now() + (30 * 24 * 60 * 60 * 1000)), // + 30 days 
      PublishDate: new Date(Date.parse('9999-12-31')),
      ExpirationDate: new Date(Date.now() + (90 * 24 * 60 * 60 * 1000)), // + 90 days
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
              StartDate: res.StartDate,
              EndDate: res.EndDate,
              PublishDate: res.PublishDate,
              ExpirationDate: res.ExpirationDate,
              Country: res.Country,
              City: res.City,
              Address: res.Address
            });
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
    console.log(formModel.StartDate);
    var activityToCreate: Activity = {
      Id: this.id,
      Title: formModel.Title,
      Description: formModel.Description,
      StartDate: formModel.StartDate,
      EndDate: new Date(Date.parse(formModel.EndDate)),
      PublishDate: new Date(Date.parse(formModel.PublishDate)),
      ExpirationDate: new Date(Date.parse(formModel.ExpirationDate)),
      Type: formModel.Type,
      Country: formModel.Country,
      City: formModel.City,
      Address: formModel.Address
    };
    console.log(activityToCreate);
    if(this.flagNewActivity)
      this._activityService.postActivityHttp(activityToCreate);
    else
      this._activityService.putActivityHttp(activityToCreate);
  }

  publish(){
    this.activityForm.controls['PublishDate'].setValue(new Date());
    this.submitForm()
  }

}
