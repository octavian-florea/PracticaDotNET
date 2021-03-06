import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs/Subscription';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { AplicationService } from '../services/aplication.service';
import { ActivityDetailsComponent } from '../activity-details/activity-details.component';
import 'rxjs/add/operator/take';
import { Status } from '../models/status.enum';
import { CompanyAplicationTable } from '../models/company-aplication-table.model';

@Component({
  selector: 'pr-aplications-student',
  templateUrl: './aplications-student.component.html',
  styleUrls: ['./aplications-student.component.css'],
  host: {'class': 'pr-full-width'}
})
export class AplicationsStudentComponent implements OnInit, OnDestroy {

  activityTableData: CompanyAplicationTable[];
  displayedColumns = ['Id','Status','ActivityType','ActivityTitle','CreatedDate']
  dataSource;
  subscriptionList: Subscription[] = [];
  
  constructor(private _aplicationService: AplicationService, public _dialog : MatDialog) {
    this._aplicationService.getAplicationsByUserHttp().take(1).subscribe(
      (res:CompanyAplicationTable[]) => {
        res.forEach(row =>{
          row.Status= Status[row.Status];
        })
        this.activityTableData = res;
        this.dataSource = new MatTableDataSource<CompanyAplicationTable>(this.activityTableData);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      (err) => { }
    )
  }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<CompanyAplicationTable>(this.activityTableData);
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
