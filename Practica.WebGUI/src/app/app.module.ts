import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCheckboxModule, MatCardModule, MatButtonModule, MatToolbarModule, MatIconModule, MatMenuModule, MatDatepickerModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatRadioModule, MatAutocompleteModule, MatDialogModule, MatPaginatorModule, MatSortModule, MatTableModule, MatProgressSpinnerModule } from '@angular/material';
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
    TeacherProfileComponent,
    ErrorDialogComponent,
    ActivitiesCompanyComponent
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
    RouterModule.forRoot([       
        { path: 'register', component:RegisterComponent },
        { path: 'login', component:LoginComponent },
        { path: 'activity/:id', component:InternshipFormComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'activities-company', component:ActivitiesCompanyComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'activity-details/:id', component:ActivityDetailsComponent },
        { path: 'profile/student', component:StudentProfileComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Student']} },
        { path: 'profile/company', component:CompanyProfileComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Company']} },
        { path: 'profile/teacher', component:TeacherProfileComponent, canActivate:[AuthenticatedGuard,RoleGuard], data: {expectedRoles:['Teacher']} },
        { path: '', component:ActivityListComponent}
    ])
  ],
  providers: [ActivityService, AuthService, ProfileService, CatalogService, AuthenticatedGuard, RoleGuard,
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
  entryComponents: [ErrorDialogComponent]
})
export class AppModule { }
