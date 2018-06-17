import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Subscription } from 'rxjs/Subscription';
import { ProfileService } from '../services/profile.service';
import { CompanyProfile } from '../models/company-profile.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'pr-view-company-profile',
  templateUrl: './view-company-profile.component.html',
  styleUrls: ['./view-company-profile.component.css']
})
export class ViewCompanyProfileComponent implements OnInit,OnDestroy {

  companyId: string;
  companyProfile: CompanyProfile;
  imageUrl: any = '';
  subscriptionList: Subscription[] = [];

  constructor(private dialogRef: MatDialogRef<ViewCompanyProfileComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _profileService:ProfileService, private _sanitizer: DomSanitizer) { 
    this.companyId = data.companyId;
    this.companyProfile = this._profileService.DefaultCompanyProfile;
    this.subscriptionList.push(this._profileService.getCompanyProfileHttpById(this.companyId).subscribe(
      (res:CompanyProfile) => { 
        this.companyProfile = res
        if(res.Logo!=null){
          this.imageUrl = this._sanitizer.bypassSecurityTrustUrl("data:image/"+res.LogoExtension+";base64," + res.Logo);
        }
      },
      (err) => {}
    ));
  }

  ngOnInit() {
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

}
