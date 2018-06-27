import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { AplicationService } from '../services/aplication.service';
import { ActivatedRoute } from '@angular/router';
import { CompanyAplicationTable } from '../models/company-aplication-table.model';
import { Status } from '../models/status.enum';
import 'rxjs/add/operator/take';
import { AplicationDetailsComponent } from '../aplication-details/aplication-details.component';

@Component({
  selector: 'pr-activities-aplications',
  templateUrl: './activities-aplications.component.html',
  styleUrls: ['./activities-aplications.component.css'],
  host: {'class': 'pr-full-width'}
})
export class ActivitiesAplicationsComponent implements OnInit, OnDestroy {

  activityTableData: CompanyAplicationTable[];
  displayedColumns = ['Id','Status','Name','Email','City','CreatedDate']
  dataSource;
  subscriptionList: Subscription[] = [];
  activityId: string;
  
  constructor(private _aplicationService: AplicationService,private _route: ActivatedRoute, public _dialog : MatDialog) {
    this.subscriptionList.push(this._route.params.subscribe( 
      params => {this.activityId = params.id;}
    ));
     
    this._aplicationService.getAplicationsByActivityHttp(this.activityId).take(1).subscribe(
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

  openAplicationDialog(aplicationId: string){
    let dialogRef = this._dialog.open(AplicationDetailsComponent, {
      data: {id: aplicationId} ,width : '90%'
    });
    dialogRef.afterClosed().take(1)
      .subscribe(() => {
        this._aplicationService.getAplicationsByActivityHttp(this.activityId).take(1).subscribe(
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
      })
  }

}
