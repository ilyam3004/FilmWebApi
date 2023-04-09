import {Component} from '@angular/core';
import {User} from "./core/models/user";
import {AccountService} from "./core/services/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent{
  user?: User | null;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  logout(){
    this.accountService.logout();
  }
}
