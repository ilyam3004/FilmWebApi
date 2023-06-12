import {Component, Input} from '@angular/core';
import {Watchlist} from "../../../../core/models/watchlist";
import {WatchlistService} from "../../../../core/services/watchlist.service";
import {first} from "rxjs";
import {AlertService} from "../../../../shared/services/alert.service";
import {Router} from "@angular/router";

@Component({
    selector: 'watchlist-table',
    templateUrl: './table.component.html',
    styleUrls: ['./table.component.scss']
})
export class TableComponent {
    @Input() watchlists: Watchlist[] | undefined;
    @Input() movieId: number = 0;

    constructor(private watchlistService: WatchlistService,
                private alertService: AlertService,
                private router: Router) { }

    removeMovieFromWatchlist(i: number, watchlist: Watchlist) {
        this.watchlistService.removeMovieFromWatchlist(watchlist.id, this.movieId)
            .pipe(first())
            .subscribe({
                next: (response: Watchlist) => {
                    this.updateWatchlist(response);
                    this.alertService
                        .success(`Successfully removed from watchlist ${watchlist.name}`);
                },
                error: error => {
                    this.alertService.error(error);
                }
            });
    }

    addMovieToWatchlist(watchlist: Watchlist) {
        this.watchlistService.addMovieToWatchlist(watchlist.id, this.movieId)
            .pipe(first())
            .subscribe({
                next: (response: Watchlist) => {
                    this.updateWatchlist(response);
                    this.alertService
                        .success(`Successfully added to watchlist ${watchlist.name}`);
                },
                error: error => {
                    this.alertService.error(error);
                }
            });
    }

    updateWatchlist(watchlist: Watchlist): void {
        const index = this.watchlists?.findIndex(w => w.id === watchlist.id);
        if (index !== undefined && index !== -1){
            this.watchlists?.splice(index, 1, watchlist);
        }
    }

    isMovieExistsInWatchlist(watchlist: Watchlist): boolean {
        return watchlist.movies.some(m => m.movie.id === this.movieId);
    }
}
