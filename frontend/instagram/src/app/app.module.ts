import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { NavheaderComponent } from './navheader/navheader.component';
import { CardComponent } from './card/card.component';
import { JwtInterceptor } from './_interceptors/jwt-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    NavheaderComponent,
    CardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule, 
    InfiniteScrollModule
  ],
  providers: [ { provide: HTTP_INTERCEPTORS, 
    useClass: JwtInterceptor,
    multi: true
  } ],
  bootstrap: [AppComponent]
})
export class AppModule { }
