import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AccountService} from "../../../core/services/account.service";

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  constructor(private router: Router,
              private accountService: AccountService
  ) {
    if (this.accountService.userValue){
      this.router.navigate(['/']);
    }
  }
}
