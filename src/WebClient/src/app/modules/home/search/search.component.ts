import { Component, OnInit } from '@angular/core';
import { MovieService } from "../../../core/services/movie.service";
import { Movie } from "../../../core/models/movie";
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})

export class SearchComponent implements OnInit {
  inputValue: string = "";
  loading: boolean = false;
  notFound: boolean = false;
  movies: Movie[] = [];

  constructor(
    private movieService: MovieService,
    private alertService: AlertService) {
  }

  ngOnInit() { }

  onSubmit() {
    if (!this.inputValue) {
      this.alertService.error("The movie title is empty.");
      this.movies = [];
      return;
    }

    this.loading = true;
    this.notFound = false;

    this.movieService.searchMovie(this.inputValue)
      .subscribe((response: Movie[]) => {
          this.movies = response;
          if(this.movies.length == 0)
            this.notFound = true;
        },

        (error) => {
          this.alertService.error(error);
        });

    this.loading = false;
  }
}
