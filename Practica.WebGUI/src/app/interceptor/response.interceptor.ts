import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";
import { tap } from "rxjs/operators";
import { MatDialog } from "@angular/material";
import { ErrorDialogComponent } from "../dialog/errorDialog.component";

@Injectable()
export class ResponseInterceptor implements HttpInterceptor{
    constructor(private router: Router, public dialog : MatDialog) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        return next.handle(request).pipe(tap(event => {
            if (event instanceof HttpResponse) {
              // do stuff with response if you want
            }
          }, (err: any) => {
            if (err instanceof HttpErrorResponse) {
              if (err.status === 401) {
                this.router.navigate(['/login']);
              }else{
                this.showError(err.error)
              }
            }
          }));
    }

    private showError(error:any): void {  
      this.dialog.open(ErrorDialogComponent, {
        data: {errorMsg: JSON.stringify(error)} ,width : '250px'
      });
    }
}