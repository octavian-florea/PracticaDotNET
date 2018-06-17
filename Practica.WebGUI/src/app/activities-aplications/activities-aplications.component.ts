import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs/Subscription';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { AplicationService } from '../services/aplication.service';
import { Aplication } from '../models/aplication.model';
import { ActivityDetailsComponent } from '../activity-details/activity-details.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'pr-activities-aplications',
  templateUrl: './activities-aplications.component.html',
  styleUrls: ['./activities-aplications.component.css'],
  host: {'class': 'pr-full-width'}
})
export class ActivitiesAplicationsComponent implements OnInit, OnDestroy {

  activityTableData: Aplication[];
  displayedColumns = ['Id','ActivityId','Status','CreatedDate']
  dataSource;
  subscriptionList: Subscription[] = [];
  activityId: string;
  
  constructor(private _aplicationService: AplicationService,private _route: ActivatedRoute, public _dialog : MatDialog) {
    this.subscriptionList.push(this._route.params.subscribe( 
      params => {this.activityId = params.id;}
    ));
     
    this.subscriptionList.push(this._aplicationService.getAplicationsByActivityHttp(this.activityId).subscribe(
      (res:Aplication[]) => {
        this.activityTableData = res;
        this.dataSource = new MatTableDataSource<Aplication>(this.activityTableData);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      (err) => { }
    ))
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Aplication>(this.activityTableData);
  }
  ngOnDestroy(){
    this.subscriptionList.forEach(sub =>{
      sub.unsubscribe;
    })
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  openActivityDialog(activityId: string){
    this._dialog.open(ActivityDetailsComponent, {
      data: {id: activityId, companyName: ''} ,width : '90%'
    });
  }

}
