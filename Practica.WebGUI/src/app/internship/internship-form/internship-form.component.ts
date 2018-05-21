import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { ActivityService } from "../../services/activity.service";


@Component({
  selector: 'app-internship-form',
  templateUrl: './internship-form.component.html',
  styleUrls: ['./internship-form.component.css']
})
export class InternshipFormComponent implements OnInit, OnDestroy {

  activityForm;

  subscriptionList: Subscription[] = [];
  
  constructor(private _formBuilder: FormBuilder, private _activityService: ActivityService) { }

  ngOnInit() {
    this.activityForm = this._formBuilder.group({
      title: ['', Validators.required],
      description:'',
      startDate: new Date(),
      endDate: new Date(),
      aplicationEndDate: new Date(),
      addres:'',
      city:'',
      seats:0
    });

    //this.subscriptionList.push(this.activityForm.controls.email.valueChanges
    //      .subscribe(value=>{
    //        console.log(value);
    //      })
    //    )
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  submitForm(){
    console.log(this.activityForm);
    const formModel = this.activityForm.value;
    var activityToCreate = {
      title: formModel.title,
      description: formModel.description,
      startDate: new Date(),
      endDate: new Date(),
      aplicationEndDate: new Date(),
      type: 'aa',
      city: 'string',
      address: 'string',
      seats: 2
    };
    
    this._activityService.createActivityHttp(activityToCreate).subscribe(
      (res) => { console.log(res) },
      (err) => { console.log(err) }
    )
  }

}
