import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import {MatPaginator, MatTableDataSource, MatSort} from '@angular/material';
import { ActivityService } from '../services/activity.service';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'pr-activities-company',
  templateUrl: './activities-company.component.html',
  styleUrls: ['./activities-company.component.css'],
  host: {'class': 'pr-full-width'}
})
export class ActivitiesCompanyComponent implements OnInit, OnDestroy {

  activityTableData: Activity[];
  displayedColumns = ['Id','Type','Title','StartDate','EndDate']
  dataSource;
  subscriptionList: Subscription[] = [];
  
  constructor(private _activityService: ActivityService) {
    this.subscriptionList.push(this._activityService.getActivitiesByUserHttp().subscribe(
      (res:Activity[]) => {
        this.activityTableData = res;
        this.dataSource = new MatTableDataSource<Activity>(this.activityTableData);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      (err) => { }
    ))
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Activity>(this.activityTableData);
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

}