import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, FormGroup, Validators  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  returnUrl: string;

  constructor(private loginFormBuilder: FormBuilder,
     private actRoute: ActivatedRoute,
      private authService: AuthService,
        private route: Router) { }

  ngOnInit() {
    this.loginForm = this.loginFormBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.returnUrl = this.actRoute.snapshot.queryParams['returnUrl'] || '/';
  }
 
  onSubmit(loginDetails) {
    console.warn('login details', loginDetails);

    if(this.loginForm.invalid){
      return;
    }
    this.authService.login(loginDetails)
      .pipe(first())
        .subscribe(
          data => {
            debugger;
            this.route.navigate([ this.returnUrl ]);
            this.loginForm.reset();
          },
          error =>{
            return;
          }
    );
  }

}
