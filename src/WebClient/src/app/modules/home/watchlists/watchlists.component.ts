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
  activeWatchlist: Watchlist = {'id': '', 'name': '', 'moviesCount': 0, 'dateTimeOfCreating': '', 'movies': []};

  constructor(private watchlistService: WatchlistService,
              private alertService: AlertService) {
  }

  ngOnInit(): void {
    // this.watchlistService.getWatchlists()
    //   .subscribe((response: Watchlist[]) => {
    //       this.watchlists = response;
    //       if (this.watchlists.length == 0)
    //         this.notFound = true;
    //     },
    //     (error) => {
    //       this.alertService.error(error);
    //     });

    this.watchlists = [
      {
        id: "isldkjd",
        name: "watchlist",
        moviesCount: 2,
        dateTimeOfCreating: "3 days ago",
        movies: [
          {
            movie: {
              "adult": false,
              "originalTitle": "Spider-Man: The Venom Saga",
              "releaseDate": "1994-06-07T00:00:00",
              "title": "Spider-Man: The Venom Sagaghhgh",
              "video": true,
              "backdropPath": "https://image.tmdb.org/t/p/original",
              "genreIds": [
                16
              ],
              "originalLanguage": "en",
              "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
              "posterPath": "https://image.tmdb.org/t/p/original/ilmsQLtthtcD8EU1k25cp0xFQ9a.jpg",
              "voteAverage": 6.7,
              "voteCount": 73,
              "id": 50410,
              "mediaType": 1,
              "popularity": 7.123
            },
            dateTimeOfAdding: "3 days ago"
          }
        ]
      },
      {
        id: "123",
        name: "watchlist1",
        moviesCount: 2,
        dateTimeOfCreating: "2 days ago",
        movies: [
          {
            movie: {
              "adult": false,
              "originalTitle": "Spider-Man: The Venom Saga",
              "releaseDate": "1994-06-07T00:00:00",
              "title": "Spider-Man: The Venom Saga",
              "video": true,
              "backdropPath": "https://image.tmdb.org/t/p/original",
              "genreIds": [
                16
              ],
              "originalLanguage": "en",
              "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
              "posterPath": "https://image.tmdb.org/t/p/original/ilmsQLtthtcD8EU1k25cp0xFQ9a.jpg",
              "voteAverage": 6.7,
              "voteCount": 73,
              "id": 50410,
              "mediaType": 1,
              "popularity": 7.123
            },
            dateTimeOfAdding: "3 days ago"
          }
        ]
      }
    ]

    this.activeWatchlist = this.watchlists[0]
  }

  activateWatchlist(watchlist: Watchlist) {
    this.activeWatchlist = watchlist;
  }
  watchlistName: string | undefined;

  removeWatchlist(): void {
    this.watchlistService.removeWatchlist(this.activeWatchlist.id)
      .subscribe(() => {
          this.alertService
              .success(`Watchlist ${this.activeWatchlist.name} was removed successfully`);
          this.watchlists = this.watchlists.filter(w => w.id !== this.activeWatchlist.id);
          this.activeWatchlist = this.watchlists[0];
        },
        (error) => {
          this.alertService.error(error);
        });
  }

  handleValueChange(watchlistName: string) {
    console.log(watchlistName)
  }
}
