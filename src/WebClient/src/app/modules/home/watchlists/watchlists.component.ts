import {Component, OnInit} from '@angular/core';
import {Watchlist} from 'src/app/core/models/watchlist';
import {WatchlistService} from 'src/app/core/services/watchlist.service';
import {AlertService} from "../../../shared/services/alert.service";
import {DatePipe} from "@angular/common";
import {first} from "rxjs";

@Component({
  selector: 'app-watchlists',
  templateUrl: './watchlists.component.html',
  styleUrls: ['./watchlists.component.scss']
})

export class WatchlistsComponent implements OnInit {
  watchlists: Watchlist[] = [];
  watchlistsNotFound: boolean = false;
  activeWatchlist: Watchlist | undefined;
  isLoading: boolean = false;

  constructor(private watchlistService: WatchlistService,
              private alertService: AlertService,
              private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.isLoading = true;

    this.watchlistService.getWatchlists()
      .subscribe((response: Watchlist[]) => {
          this.watchlists = response;
          this.isLoading = false;
          if (this.watchlists.length == 0) {
            this.watchlistsNotFound = true;
          }
        },
        (error) => {
          this.alertService.error(error);
        });
  }

  activateWatchlist(watchlist: Watchlist) {
    this.activeWatchlist = watchlist;
  }

  removeWatchlist(): void {
    if(this.activeWatchlist){
      this.watchlistService.removeWatchlist(this.activeWatchlist.id)
        .subscribe(() => {
            this.alertService
              .success(`Watchlist ${this.activeWatchlist?.name} removed successfully`);
            this.watchlists = this.watchlists.filter(w => w.id !== this.activeWatchlist?.id);
            this.activeWatchlist = this.watchlists[0];
            if(this.watchlists.length === 0){
              this.watchlistsNotFound = true;
            }
          },
          (error) => {
            this.alertService.error(error);
          });
    }
  }

  handleValueChange(watchlistName: string) {
    this.createWatchlist(watchlistName);
  }

  createWatchlist(watchlistName: string){
    this.watchlistService.createWatchlist(watchlistName)
      .subscribe((watchlist: Watchlist) => {
        this.watchlists.push(watchlist)
        this.alertService
          .success(`Watchlist ${watchlist.name} added successfully`);
        this.watchlistsNotFound = false;
      },
      (error) => {
        this.alertService.error(error);
      });;
  }

  getFormattedDate(date: string): string{
    return this.datePipe
      .transform(date, 'MMM d, y, h:mm a') || "";
  }

  removeMovieFromWatchlist(id: number){
    this.watchlistService.removeMovieFromWatchlist(this.activeWatchlist!.id, id)
      .pipe(first())
      .subscribe({
        next: (response: Watchlist) => {
          this.updateWatchlist(response);
          this.alertService
            .success(`Successfully removed from watchlist ${this.activeWatchlist!.name}`);
        },
        error: error => {
          this.alertService.error(error);
        }
      });
  }

  updateWatchlist(watchlist: Watchlist): void {
    const index = this.watchlists?.findIndex(w => w.id === watchlist.id);
    if (index !== undefined && index !== -1){
      this.watchlists.splice(index, 1, watchlist);
    }
  }
}
