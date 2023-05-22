import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { AlertComponent } from './shared/components/alert/alert.component';
import {ReactiveFormsModule} from "@angular/forms";
import {ApiInterceptor} from "./shared/helpers/api-interceptor.service";
import {ErrorInterceptor} from "./shared/helpers/error.interceptor";
import {DatePipe} from "@angular/common";

@NgModule({
  declarations: [
    AppComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      DatePipe
  ],
    exports: [
        AlertComponent
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
