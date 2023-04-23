import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { BehaviorSubject, map, Observable } from "rxjs";
import { RegisterRequest, User } from "../models/user";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment.development";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSubject: BehaviorSubject<User | null>;
  public user: Observable<User | null>;

  constructor(
    private router: Router,
    private http: HttpClient
  ) {
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  public get userValue() {
    return this.userSubject.value;
  }

  login(login: string, password: string) {
    return this.http.post<User>(`${environment.apiBaseUrl}/users/login`, { login, password })
      .pipe(map(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['account/login']);
  }

  register(request: RegisterRequest) {
    return this.http.post(
      `${environment.apiBaseUrl}/users/register`,
      request, {withCredentials: true});
  }
}
