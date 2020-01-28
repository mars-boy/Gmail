import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import * as jwt_decode from 'jwt-decode';

import { AuthService } from '../_services/auth.service';
import { switchMap, finalize, filter, take } from 'rxjs/operators';
import { User } from '../models/UserDetails';
import { DateFunctions } from '../_utilities/date-functions'; 

@Injectable()
export class JwtInterceptor implements HttpInterceptor {


    constructor(private authService: AuthService, private dateFunctions: DateFunctions){

    }

    private isTokenGettingRefreshed : boolean = false;
    private tokenBehavior : BehaviorSubject<string> = new BehaviorSubject<string>(null);

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        debugger;
        let headers = req.headers;
        if(!req.headers.has('Jwt_Interseptor_Skip')){
            let loggedInUser = this.authService.getUser();
            let token = loggedInUser.token;
            var decoded = jwt_decode(token);
            if(decoded['exp'] >=  this.dateFunctions.getUtcTicks()){
                if(!this.isTokenGettingRefreshed){
                    this.isTokenGettingRefreshed = true;
                    this.tokenBehavior.next(null);

                    return this.authService.refreshToken(loggedInUser)
                        .pipe(
                            switchMap(( refreshUser: User )=>{
                                if(refreshUser){
                                    this.tokenBehavior.next(refreshUser.token);
                                    return next.handle(this.addTokenToRequest(req, refreshUser.token));
                                }
                            }), finalize(()=>{
                                this.isTokenGettingRefreshed = false;
                            })
                        )
                }
                else{
                    return this.tokenBehavior.pipe(filter(token => token!=null), 
                        take(1),
                        switchMap(token => {
                            return next.handle(this.addTokenToRequest(req, token));
                        })
                    )
                }
            }else{
                return this.tokenBehavior.pipe(filter(token => token!=null), 
                        take(1),
                        switchMap(token => {
                            return next.handle(this.addTokenToRequest(req, token));
                        })
                )
            }
        }else{
            return next.handle(req);
        }
    }

    addTokenToRequest(request: HttpRequest<any>, token: string){
        return request.clone({setHeaders: { Authorization: token }});
    }

}
