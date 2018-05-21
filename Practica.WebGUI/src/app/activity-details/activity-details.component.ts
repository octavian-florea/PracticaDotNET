import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ActivityService } from '../services/activity.service';

@Component({
  selector: 'app-activity-details',
  templateUrl: './activity-details.component.html',
  styleUrls: ['./activity-details.component.css']
})
export class ActivityDetailsComponent implements OnInit {
    id: string;
    activity: object;

  constructor(private _route: ActivatedRoute, private _activityService: ActivityService) {
    this._route.paramMap
      .subscribe(parms =>{
        this.id = parms.get("id");
        this.activity = this._activityService.get(this.id);
      })
  }

  ngOnInit() {

  }

}
