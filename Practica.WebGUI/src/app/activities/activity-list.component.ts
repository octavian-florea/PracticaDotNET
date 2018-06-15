import { Component } from "@angular/core";
import { ActivityService } from "../services/activity.service";

@Component({
    selector:'pr-activities',
    templateUrl:'./activity-list.component.html',
    styleUrls:['./activity-list.component.css']
})
export class ActivityListComponent{
    activities: any[];
    testConnection: String;

    constructor(private _activityService: ActivityService){
        
    }
    ngOnInit():void{
        this.activities = this._activityService.getActivity();
        this._activityService.getActivitiesHttp().subscribe(
            (res) => { console.log(res) },
            (err) => { console.log(err) }
        )
    }
}