import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          if (error.error instanceof ErrorEvent) {
            console.error("Error Event");
          } else {
            console.log(`error status : ${error.status} ${error.statusText}`);
            switch (error.status) {
              case 0:
                console.log("server-error");
                this.router.navigateByUrl("/server-error");
                break;
              case 401:
                console.log("login");
                this.router.navigateByUrl("/login");
                break;
              case 403:
                console.log("login");
                this.router.navigateByUrl("/login");
                break;
              case 404:
                console.log("notfound-error");
                this.router.navigateByUrl("/notfound-error");
                break;
              case 500:
                console.log("server-error");
                this.router.navigateByUrl("/server-error");
                break;
            }
          }
        } else {
          console.error("Somethings else happened.");
        }
        return throwError(error);
      })
    );
  }
}
