import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class RequestInterceptor implements HttpInterceptor{
    constructor() {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${localStorage.getItem('userToken')}`
            }
        });
      
        return next.handle(request);
    }
}