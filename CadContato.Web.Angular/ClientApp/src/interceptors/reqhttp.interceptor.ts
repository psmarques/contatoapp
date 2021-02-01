import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { LoaderService } from "../services/loader.service";
import { SessionService } from "../services/session.service";

@Injectable()
export class ReqHttpInterceptor implements HttpInterceptor {
  constructor(public loaderService: LoaderService, private session:SessionService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    this.loaderService.show();

    const u = this.session.getUser();

    const newReq = req.clone({
      headers: req.headers.set('Bearer', (u != null ? u.idToken : 'notlogged'))
    });

    console.log(newReq);

    return next.handle(newReq).pipe(finalize(() => {

      console.log("error");
      this.loaderService.hide();

    }));

  }
}
