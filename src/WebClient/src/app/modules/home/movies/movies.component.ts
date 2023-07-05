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

  slideConfig = { slidesToShow: 3, slidesToScroll: 3 };
  addSlide() {

  }
  removeSlide() {
  }
  slickInit(e: any) {
    console.log('slick initialized');
  }
  breakpoint(e: any) {
    console.log('breakpoint');
  }
  afterChange(e: any) {
    console.log('afterChange');
  }
  beforeChange(e: any) {
    console.log('beforeChange');
  }

  constructor(private movieService: MovieService,
              private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.getPopularMovies();
    this.getTopRatedMovies();
    this.getUpcomingMovies();
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
          this.isLoading = false;
        },
        (error) => {
          this.alertService.error(error)
        });
  }
}
