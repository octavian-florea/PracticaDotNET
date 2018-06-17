import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivityService } from "../services/activity.service";
import { Subscription } from "rxjs/Subscription";
import { ActivityCard } from "../models/activity-card.model";
import { DomSanitizer } from "@angular/platform-browser";
import { Search } from "../models/search.model";
import { MatDialog } from "@angular/material";
import { ActivityDetailsComponent } from "../activity-details/activity-details.component";
import { ViewCompanyProfileComponent } from "../profile/view-company-profile.component";

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
    flagNoSearchResults = false;
    flagSearching = false;

    constructor(private _activityService: ActivityService, private _sanitizer: DomSanitizer, public _dialog : MatDialog){
        
    }
    ngOnInit():void{
        this.subscriptionList.push(
            this._activityService.searchObserver.subscribe(
                (searchObj:Search) =>{
                    this.flagSearching = true;
                    this.flagNoSearchResults = false;
                    this.activityCards = [];
                    this.searchForActivities(searchObj);
                }
            )
        );

        
    }
    ngOnDestroy(){
        this.subscriptionList.forEach(sub =>{
          sub.unsubscribe;
        })
      }

    private searchForActivities(searchObj: Search){
        this.subscriptionList.push(this._activityService.getActivitiesHttp(searchObj).subscribe(
            (res:any[]) => { 
                for(var elem of res){
                    this.activityCards.push({
                        Id: elem.Id,
                        Title: elem.Title,
                        Type: this.firstLetterUpercase(elem.Type),
                        Description: elem.Description,
                        City: elem.City,
                        CompanyId: elem.CompanyId,
                        CompanyName: elem.CompanyName, 
                        CompanyImageUrl: this.getCompanyImageUrl(elem)
                    });
                }
                this.flagSearching = false;
                if(this.activityCards.length==0)
                this.flagNoSearchResults = true;
            },
            (err) => { }
        ))
    }

    getCompanyImageUrl (elem): any{
        if(elem.CompanyLogo == null || elem.CompanyLogo == "")
            return ""
        else
            return this._sanitizer.bypassSecurityTrustUrl("data:image/"+elem.CompanyLogoExtension.replace(".","")+";base64," + elem.CompanyLogo)
    }

    private firstLetterUpercase(value: string): string{
        return value.charAt(0).toUpperCase() + value.slice(1).toLowerCase()
    }

    openActivityDialog(id,companyName){
        this._dialog.open(ActivityDetailsComponent, {
            data: {id: id, companyName: companyName} ,width : '90%'
          });
    }

    openCompanyDialog(companyId){
        this._dialog.open(ViewCompanyProfileComponent, {
            data: {companyId: companyId} ,width : '90%'
          });
    }
}