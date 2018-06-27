import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCheckboxModule, MatCardModule, MatButtonModule, MatToolbarModule, MatIconModule, MatMenuModule, MatDatepickerModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatRadioModule, MatAutocompleteModule, MatDialogModule, MatPaginatorModule, MatSortModule, MatTableModule, MatProgressSpinnerModule, MatProgressBarModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { ActivityListComponent } from './activities/activity-list.component';
import { ActivityDetailsComponent } from './activity-details/activity-details.component';
import { CardComponent } from './card/card.component';
import { RegisterComponent } from './register/register.component';
import { InternshipFormComponent } from './internship/internship-form/internship-form.component';
import { LoginComponent } from './login/login.component';
import { StudentProfileComponent } from './profile/studentProfile.component';
import { TeacherProfileComponent } from './profile/teacherProfile.component';
import { CompanyProfileComponent } from './profile/companyProfile.component';
import { ActivityService } from './services/activity.service';
import { AuthService } from './services/auth.service';
import { ProfileService } from './services/profile.service';
import { CatalogService } from './services/catalog.service';
import { ErrorDialogComponent } from './dialog/errorDialog.component';
import { AuthenticatedGuard } from './guard/authenticated.guard';
import { RoleGuard } from './guard/role.guard';
import { RequestInterceptor } from './interceptor/request.interceptor';
import { ResponseInterceptor } from './interceptor/response.interceptor';
import { ActivitiesCompanyComponent } from './activities-company/activities-company.component';
import { MatMomentDateModule } from '@angular/material-moment-adapter'
import * as moment from 'moment';
import { ViewCompanyProfileComponent } from './profile/view-company-profile.component';
import { AplicationService } from './services/aplication.service';
import { AplicationsStudentComponent } from './aplications-student/aplications-student.component';
import { ActivitiesAplicationsComponent } from './activities-aplications/activities-aplications.component';
import { AutocompleatService } from './services/autocompleat.service';
import { AplicationDetailsComponent } from './aplication-details/aplication-details.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { StatisticsService } from './services/statistics.service';

@NgModule({
  declarations: [
    AppComponent,
    ActivityListComponent,
    ActivityDetailsComponent,
    AplicationDetailsComponent,
    CardComponent,
    RegisterComponent,
    InternshipFormComponent,
    LoginComponent,
    StudentProfileComponent,
    CompanyProfileComponent,
    TeacherProfileComponent,
    ErrorDialogComponent,
    ActivitiesCompanyComponent,
    ViewCompanyProfileComponent,
    AplicationsStudentComponent,
    ActivitiesAplicationsComponent,
    StatisticsComponent
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
    MatMomentDateModule,
    MatSelectModule,
    MatAutocompleteModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatDialogModule,
    FlexLayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    RouterModule.forRoot([       
        { path: 'register', component:RegisterComponent },
        { path: 'login', component:LoginComponent },
        { path: 'aplications/activity/:id', component:ActivitiesAplicationsComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'aplications', component:AplicationsStudentComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Student']} },
        { path: 'activity/:id', component:InternshipFormComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'activities-company', component:ActivitiesCompanyComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'activity-details/:id', component:ActivityDetailsComponent },
        { path: 'profile/student', component:StudentProfileComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Student']} },
        { path: 'profile/company', component:CompanyProfileComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'profile/teacher', component:TeacherProfileComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Teacher']} },
        { path: 'statistics', component:StatisticsComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Teacher']} },
        { path: '', component:ActivityListComponent}
    ])
  ],
  providers: [ActivityService, AuthService, ProfileService, CatalogService, AuthenticatedGuard, RoleGuard, AplicationService, AutocompleatService, StatisticsService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ResponseInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [ErrorDialogComponent,ActivityDetailsComponent,ViewCompanyProfileComponent,AplicationDetailsComponent]
})
export class AppModule { }
