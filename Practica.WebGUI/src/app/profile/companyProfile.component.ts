import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup  } from '@angular/forms';
import 'rxjs/add/operator/takeLast';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from "../services/auth.service";
import { ProfileService } from '../services/profile.service';
import { CompanyProfile } from '../models/company-profile.model';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'pr-company-profile',
  templateUrl: './companyProfile.component.html',
  styleUrls: ['./companyProfile.component.css'],
  host: {'class': 'pr-full-width'}
})
export class CompanyProfileComponent implements OnInit, OnDestroy {

  profileForm: FormGroup;
  imageUrl:any ="";
  fileToUpload: File;

  subscriptionList: Subscription[] = [];
  
  constructor(private formBuilder: FormBuilder, private _profileService:ProfileService, private _sanitizer: DomSanitizer) { }

  ngOnInit() {

    this.profileForm = this.formBuilder.group({
      companyName:'',
      companyDescription:'',
      website:'',
      address:''
    });

    this._profileService.getCompanyProfileHttp().subscribe(
      (res:CompanyProfile) => { 
          console.log(res);
          this.profileForm.setValue({
            companyName: res.Name,
            companyDescription: res.Description,
            website: res.Website,
            address: res.Adress
          });

          if(res.Logo!=null){
            this.imageUrl = this._sanitizer.bypassSecurityTrustUrl("data:image/"+res.LogoExtension+";base64," + res.Logo);
          }
      },
      (err) => {}
    );

    
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  handleFileInput(file: FileList){
    this.fileToUpload = file.item(0);

    var reader = new FileReader();
    reader.onload = (event:any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }

  submitForm(){
    const formModel = this.profileForm.value;
    let companyProfile: CompanyProfile;
    companyProfile = {
      Logo:this.fileToUpload,
      LogoExtension: "",
      Name: formModel.companyName,
      Description: formModel.companyDescription,
      Website: formModel.website,
      Adress: formModel.address
    };

    this._profileService.postCompanyProfileHttp(companyProfile);
    
  }
}
