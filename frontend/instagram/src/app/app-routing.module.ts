import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

import { AuthGuard } from './_guards/auth.guard';
import { UploadComponent } from './upload/upload.component';


const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [ AuthGuard ] },
  { path: 'login', component: LoginComponent },
  { path: 'upload', component: UploadComponent, canActivate:[ AuthGuard ] },


  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
