import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./modules/home/home.component";
import {AuthGuard} from "./shared/helpers/auth.guard";
import {RegisterComponent} from "./modules/auth/register/register.component";

const authModule = () => import('./modules/auth/auth.module')
  .then(x => x.AuthModule);

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'account', loadChildren: authModule },
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
