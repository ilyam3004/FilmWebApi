import { Component } from '@angular/core';
import {NavigationEnd, Router} from "@angular/router";
import {AccountService} from "../../../core/services/account.service";

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  public currentRoute: string;

  constructor(private router: Router,
              private userService: AccountService) {
    this.currentRoute = "";
  }

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.urlAfterRedirects;
      }
    })
  }

  logOut():void{
    this.userService.logout();
  }
}
