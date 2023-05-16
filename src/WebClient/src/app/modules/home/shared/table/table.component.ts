import {Component, Input} from '@angular/core';
import {Watchlist} from "../../../../core/models/watchlist";

@Component({
  selector: 'watchlist-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent {
  @Input() watchlists: Watchlist[] | undefined;
  @Input() movieId: number | undefined;
  
  deleteWatchlist(i: number, watchlist: Watchlist) {
    console.log(watchlist)
  }
  
  addMovieToWatchlist(watchlist: Watchlist) {
    
  }

  isMovieExistsInWatchlist(watchlist: Watchlist): boolean{
    return watchlist.movies.some(m => m.id === this.movieId);
  }
}
