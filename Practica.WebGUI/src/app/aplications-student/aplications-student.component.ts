import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Activity } from '../models/activity.model';
import { Subscription } from 'rxjs/Subscription';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { AplicationService } from '../services/aplication.service';
import { Aplication } from '../models/aplication.model';
import { ActivityDetailsComponent } from '../activity-details/activity-details.component';

@Component({
  selector: 'pr-aplications-student',
  templateUrl: './aplications-student.component.html',
  styleUrls: ['./aplications-student.component.css'],
  host: {'class': 'pr-full-width'}
})
export class AplicationsStudentComponent implements OnInit, OnDestroy {

  activityTableData: Aplication[];
  displayedColumns = ['Id','ActivityId','Status','CreatedDate']
  dataSource;
  subscriptionList: Subscription[] = [];
  
  constructor(private _aplicationService: AplicationService, public _dialog : MatDialog) {
    this.subscriptionList.push(this._aplicationService.getAplicationsByUserHttp().subscribe(
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
