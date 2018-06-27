import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '../services/statistics.service';
import 'rxjs/add/operator/take';

@Component({
  selector: 'pr-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css'],
  host: {'class': 'pr-full-width'}
})
export class StatisticsComponent implements OnInit {

  nrStudents = 0;
  nrCompanies = 0;
  nrActiveInterships = 0;
  nrActiveTrainings = 0;
  nrActiveEvents = 0;

  constructor(private _statisticsService: StatisticsService) { }
  
  ngOnInit() {
    this._statisticsService.getNrStudentsHttp().take(1).subscribe(
      (res:number) => { this.nrStudents = res },
      (err) => {}
    );

    this._statisticsService.getNrCompaniesHttp().take(1).subscribe(
      (res:number) => { this.nrCompanies = res },
      (err) => {}
    );

    this._statisticsService.getNrActiveActivitiesHttp("practica").take(1).subscribe(
      (res:number) => { this.nrActiveInterships = res },
      (err) => {}
    );

    this._statisticsService.getNrActiveActivitiesHttp("curs").take(1).subscribe(
      (res:number) => { this.nrActiveTrainings = res },
      (err) => {}
    );

    this._statisticsService.getNrActiveActivitiesHttp("eveniment").take(1).subscribe(
      (res:number) => { this.nrActiveEvents = res },
      (err) => {}
    );
  }

}
