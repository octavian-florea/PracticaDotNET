import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';


@Injectable()
export class LoadingService{

    private isLoading = new BehaviorSubject<boolean>(false);
    private loadingPercent = new BehaviorSubject<number>(0);

    get IsLoading(): Observable<boolean> {
        return this.isLoading.asObservable();
    }
    get LoadingPercent(): Observable<number> {
        return this.loadingPercent.asObservable();
    }
    
    constructor(){}

    startLoading() {
        this.isLoading.next(true);
        this.loadingPercent.next(0);
        this.incresePercent(0);
    }

    stopLoading() {
        this.isLoading.next(false);
    }

    private incresePercent(percent:number){
        while(percent<100){
            percent += 20
            setTimeout( () => {
                this.loadingPercent.next(percent);
            },500)
        }
        this.loadingPercent.next(0);
    }
}