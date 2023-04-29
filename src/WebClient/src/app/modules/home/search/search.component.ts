import {Component, OnInit} from '@angular/core';
import {MovieService} from "../../../core/services/movie.service";
import {Movie} from "../../../core/models/movie";
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  inputValue: string = "";
  loading: boolean = false;
  movies: Movie[] = [];

  constructor(
    private movieService: MovieService,
    private alertService: AlertService) { }

  ngOnInit() { }

  processKeyup(query: string){
    if(!query){
      this.movies = [];
      return;
    }
    this.loading = true;
    this.movieService.searchMovie(query)
      .subscribe(
        (response: Movie[]) => {
          this.movies = response;
          this.loading = false;
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        }
      );
  }
}
