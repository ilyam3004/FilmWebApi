import {Component, OnInit} from '@angular/core';
import {Watchlist} from 'src/app/core/models/watchlist';
import {WatchlistService} from 'src/app/core/services/watchlist.service';
import {AlertService} from "../../../shared/services/alert.service";

@Component({
  selector: 'app-watchlists',
  templateUrl: './watchlists.component.html',
  styleUrls: ['./watchlists.component.scss']
})
export class WatchlistsComponent implements OnInit {
  watchlists: Watchlist[] = [];
  notFound: boolean = false;

  constructor(private watchlistService: WatchlistService,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.watchlistService.getWatchlists()
      .subscribe((response: Watchlist[]) => {
          this.watchlists = response;
          if (this.watchlists.length == 0)
            this.notFound = true;
        },
        (error) => {
          this.alertService.error(error);
        });
  }
}
