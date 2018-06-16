import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivityService } from "../services/activity.service";
import { Subscription } from "rxjs/Subscription";
import { ActivityCard } from "../models/activity-card.model";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
    selector:'pr-activities',
    templateUrl:'./activity-list.component.html',
    styleUrls:['./activity-list.component.css'],
    host: {'class': 'pr-full-width'}
})
export class ActivityListComponent implements OnInit, OnDestroy{
    activityCards: ActivityCard[] = [];
    testConnection: String;
    subscriptionList: Subscription[] = [];

    constructor(private _activityService: ActivityService, private _sanitizer: DomSanitizer){
        
    }
    ngOnInit():void{
        this.subscriptionList.push(this._activityService.getActivitiesHttp().subscribe(
            (res:any[]) => { 
                for(var elem of res){
                    this.activityCards.push({
                        Id: elem.Id,
                        Title: elem.Title,
                        Type: elem.Type,
                        Description: elem.Description,
                        City: elem.City,
                        CompanyName: elem.CompanyName, 
                        CompanyImageUrl: this.getCompanyImageUrl(elem)
                    });
                }
            },
            (err) => { console.log(err) }
        ))
    }
    ngOnDestroy(){
        this.subscriptionList.forEach(sub =>{
          sub.unsubscribe;
        })
      }

    getCompanyImageUrl (elem): any{
        if(elem.CompanyLogo == null || elem.CompanyLogo == "")
            return ""
        else
            return this._sanitizer.bypassSecurityTrustUrl("data:image/"+elem.CompanyLogoExtension.replace(".","")+";base64," + elem.CompanyLogo)
    }
}