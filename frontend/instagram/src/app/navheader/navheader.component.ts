import { Component, OnInit } from '@angular/core';

import { AuthService } from '../_services/auth.service';
import {} from '../models/UserDetails';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navheader',
  templateUrl: './navheader.component.html',
  styleUrls: ['./navheader.component.css']
})
export class NavheaderComponent implements OnInit {


  public loggedIn: boolean;
  public loggedUserName: string;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {

    this.loggedIn = this.authService.getUser() ? true : false;
    this.loggedUserName = this.loggedIn ? this.authService.getUser()['userName'] : 'ghost';

    this.authService.getLoggedInUser.subscribe( 
      data => {
        this.loggedIn = data ? true : false;
        this.loggedUserName = this.loggedIn ? data['userName'] : 'ghost';
      }
     );
  }

  logout(){
    debugger;
    this.authService.logout().subscribe( data => {
      this.router.navigate(['login']);
    } );
  }



}
