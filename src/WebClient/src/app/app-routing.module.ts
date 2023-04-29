import { RouterModule, Routes } from '@angular/router';
import {AuthGuard} from "./shared/helpers/auth.guard";
import { NgModule } from '@angular/core';

const authModule = () => import('./modules/auth/auth.module')
  .then(x => x.AuthModule);
const homeModule = () => import('./modules/home/home.module')
  .then(x => x.HomeModule);

const routes: Routes = [
  { path: '', loadChildren: homeModule, canActivate: [AuthGuard]},
  { path: 'account', loadChildren: authModule },
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
