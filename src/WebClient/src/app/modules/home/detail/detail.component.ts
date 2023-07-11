import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MovieDetails} from "../../../core/models/movie";
import {MovieService} from "../../../core/services/movie.service";
import {AlertService} from "../../../shared/services/alert.service";
import {Watchlist} from "../../../core/models/watchlist";
import {WatchlistService} from "../../../core/services/watchlist.service";
import {first} from "rxjs";


@Component({
  selector: 'movie-details',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})

export class DetailComponent implements OnInit {
  movie: MovieDetails | undefined;
  player: YT.Player | undefined;
  watchlists: Watchlist[] | undefined;
  movieId: number = 0;
  isLoading: boolean = false;

  constructor(private route: ActivatedRoute,
              private movieService: MovieService,
              private alertService: AlertService,
              private watchlistService: WatchlistService) {
  }

  ngOnInit() {
    this.isLoading = true;
    this.route.queryParams.subscribe(params => {
      this.movieId = params.id;
      this.getWatchlistsData();
      this.getMovieData();
    });
  }

  getWatchlistsData() {
    this.watchlistService.getWatchlists()
      .pipe(first())
      .subscribe({
        next: (response: Watchlist[]) => {
          this.watchlists = response;
        },
        error: error => {
          this.alertService.error(error);
        }
      });
  }

  getMovieData() {
    this.movieService.getMovieDetails(this.movieId)
      .subscribe((response: MovieDetails) => {
          this.movie = response
          this.isLoading = false;
        },
        (error) => {
          this.alertService.error(error);
        });
  }

  handleImageError(event: any) {
    event.target.src = "./assets/img/placeholder.jpg"
  }

  savePlayer(player: YT.Player) {
    this.player = player;
  }

  onStateChange(event: any) {
    console.log('player state', event.data);
  }
}
