import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Login } from '../models/LoginDetails';
import { User } from '../models/UserDetails';
import { async } from '@angular/core/testing';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  @Output() getLoggedInUser: EventEmitter<User> = new EventEmitter(true);

  private currentUserSubject: BehaviorSubject<User>;
  private currentUser: Observable<User>;

  constructor(private http: HttpClient ) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('userdetails')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(loginModel: Login){
    var changedHeaders = new HttpHeaders({
      'Jwt_Interseptor_Skip': '1'
    });
    debugger;
    return this.http.post<any>('http://localhost:5000/api/Auth/Login',{ loginModel } , { headers: changedHeaders })
      .pipe(map( user => {
        if(user && user.token){
          debugger;
          localStorage.setItem('userdetails', JSON.stringify(user));
          this.getLoggedInUser.emit(user);
          this.currentUserSubject.next(user);
        }
        return user.token;
      }))
  }

  refreshToken(userDetails: User){
    var changedHeaders = new HttpHeaders();
    changedHeaders.set('Jwt_Interseptor_Skip', '1');
    return this.http.post<any>('http://localhost:5000/api/Auth/RefreshToken',
      { userDetails }, { headers : changedHeaders} )
      .pipe(map( usr => {
        if(usr && usr.token){
          localStorage.setItem('userdetails', JSON.stringify(usr));
          this.currentUserSubject.next(usr);
        }
        return usr;
      }))
  }

  getUser(): User {
    return this.currentUserSubject.value;
  }

  logout(){
    localStorage.removeItem('userdetails');
    this.getLoggedInUser.emit(null);
    this.currentUserSubject.next(null);
    return new Observable<boolean>((observer) => observer.next(true));
  }
}
