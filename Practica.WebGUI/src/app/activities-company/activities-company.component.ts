import { Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator, MatTableDataSource, MatSort} from '@angular/material';
import { ActivityService } from '../services/activity.service';
import { Activity } from '../models/activity.model';

@Component({
  selector: 'pr-activities-company',
  templateUrl: './activities-company.component.html',
  styleUrls: ['./activities-company.component.css'],
  host: {'class': 'pr-full-width'}
})
export class ActivitiesCompanyComponent implements OnInit {

  activityTableData: Activity[];
  displayedColumns = ['Id','Type','Title','StartDate','EndDate']
  dataSource;
  
  constructor(private _activityService: ActivityService) {
    this._activityService.getActivitiesByUserHttp().subscribe(
      (res:Activity[]) => {
        this.activityTableData = res;
        this.dataSource = new MatTableDataSource<Activity>(this.activityTableData);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      (err) => { }
    )
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Activity>(this.activityTableData);
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

}