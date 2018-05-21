import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCheckboxModule, MatCardModule, MatButtonModule, MatToolbarModule, MatIconModule, MatMenuModule, MatDatepickerModule, MatFormFieldModule, MatInputModule, MatNativeDateModule, MatSelectModule, MatRadioModule, MatAutocompleteModule } from '@angular/material';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ActivityListComponent } from './activities/activity-list.component';
import { ActivityDetailsComponent } from './activity-details/activity-details.component';
//import { InternshipModule } from './internship/internship.module';
//import { SharedModule } from './shared/shared.module';
import { CardComponent } from './card/card.component';
import { RegisterComponent } from './register/register.component';
import { InternshipFormComponent } from './internship/internship-form/internship-form.component';
import { LoginComponent } from './login/login.component';
import { StudentProfileComponent } from './profile/studentProfile.component';
import { TeacherProfileComponent } from './profile/teacherProfile.component';
import { CompanyProfileComponent } from './profile/companyProfile.component';

@NgModule({
  declarations: [
    AppComponent,
    ActivityListComponent,
    ActivityDetailsComponent,
    CardComponent,
    RegisterComponent,
    InternshipFormComponent,
    LoginComponent,
    StudentProfileComponent,
    CompanyProfileComponent,
    TeacherProfileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatAutocompleteModule,
    //InternshipModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    RouterModule.forRoot([
        { path: 'activities/:id', component:ActivityDetailsComponent},
        { path: 'register', component:RegisterComponent},
        { path: 'login', component:LoginComponent},
        { path: 'activities/new', component:InternshipFormComponent},
        { path: 'profile/student', component:StudentProfileComponent},
        { path: 'profile/company', component:CompanyProfileComponent},
        { path: 'profile/teacher', component:TeacherProfileComponent},
        { path: '', component:ActivityListComponent}
    ]),
    //SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
