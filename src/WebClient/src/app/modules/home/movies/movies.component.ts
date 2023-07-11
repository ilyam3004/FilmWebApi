import {Component, OnInit} from '@angular/core';
import {Movie} from '../../../core/models/movie';
import {MovieService} from '../../../core/services/movie.service';
import {AlertService} from '../../../shared/services/alert.service';


@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})

export class MoviesComponent implements OnInit {
  isLoading: boolean = false;
  popularMovies: Movie[] = [];
  topRatedMovies: Movie[] = [];
  upcomingMovies: Movie[] = [];
  nowPlayingMovies: Movie[] = [];

  constructor(private movieService: MovieService,
              private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.getPopularMovies();
    this.getTopRatedMovies();
    this.getUpcomingMovies();
    this.getNowPlayingMovies();
  }

  getPopularMovies(): void {
    this.movieService.getPopularMovies()
      .subscribe((response: Movie[]) => {
          this.popularMovies = response;
        },
        (error) => {
          this.alertService.error(error)
        });
  }

  getTopRatedMovies(): void {
    this.movieService.getTopRatedMovies()
      .subscribe((response: Movie[]) => {
          this.topRatedMovies = response;
        },
        (error) => {
          this.alertService.error(error)
        });
  }

  getUpcomingMovies(): void {
    this.movieService.getUpcomingMovies()
      .subscribe((response: Movie[]) => {
          this.upcomingMovies = response;
        },
        (error) => {
          this.alertService.error(error)
        });
  }

  getNowPlayingMovies(): void {
    this.movieService.getNowPlayingMovies()
      .subscribe((response: Movie[]) => {
          this.nowPlayingMovies = response;
          this.isLoading = false;
        },
        (error) => {
          this.isLoading = true;
          this.alertService.error(error);
        })
  }
}
