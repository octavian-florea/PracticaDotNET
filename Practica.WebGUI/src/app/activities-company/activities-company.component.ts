import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import {MatPaginator, MatTableDataSource, MatSort} from '@angular/material';
import { ActivityService } from '../services/activity.service';
import { Subscription } from 'rxjs';
import { CompanyActivityTable } from '../models/company-activity-table.model';

@Component({
  selector: 'pr-activities-company',
  templateUrl: './activities-company.component.html',
  styleUrls: ['./activities-company.component.css'],
  host: {'class': 'pr-full-width'}
})
export class ActivitiesCompanyComponent implements OnInit, OnDestroy {

  activityTableData: CompanyActivityTable[] = [];
  displayedColumns = ['Id','Type','Title','StartDate','EndDate','PublishDate','ExpirationDate','City','NrAplications']
  dataSource;
  subscriptionList: Subscription[] = [];
  
  constructor(private _activityService: ActivityService) {
    this.subscriptionList.push(this._activityService.getActivitiesByUserHttp().subscribe(
      (res:any[]) => {
        res.forEach( (element) => {
          let row:CompanyActivityTable = {
            Id: element.Id,
            Title: element.Title,
            Type: element.Type,
            StartDate: element.StartDate,
            EndDate: element.EndDate,
            PublishDate: element.PublishDate,
            ExpirationDate: element.ExpirationDate,
            City: element.City,
            NrAplications: element.Aplications.length
          }
          this.activityTableData.push(row);
        });
         

        //this.activityTableData = res;
        this.dataSource = new MatTableDataSource<CompanyActivityTable>(this.activityTableData);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      (err) => { }
    ))
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<CompanyActivityTable>(this.activityTableData);
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