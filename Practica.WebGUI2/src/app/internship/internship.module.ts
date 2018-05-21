import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
//import { InternshipFormComponent } from './internship-form/internship-form.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
     // { path: 'practica/new', component:InternshipFormComponent}
    ]),
  ],
  declarations: [
    //InternshipFormComponent
  ]
})
export class InternshipModule { }
