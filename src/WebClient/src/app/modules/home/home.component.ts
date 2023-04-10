import { Component } from '@angular/core';
import {User} from "../../core/models/user";
import {AccountService} from "../../core/services/account.service";

@Component({
  templateUrl: './home.component.html',
})
export class HomeComponent {
  user: User | null;

  constructor(private accountService: AccountService) {
    this.user = this.accountService.userValue;
  }
}
