import {Component} from '@angular/core';
import {MovieService} from "../../../core/services/movie.service";
import {Movie} from "../../../core/models/movie";
import { AlertService } from 'src/app/shared/services/alert.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  inputValue: string = "";
  movies: Movie[] = [];

  constructor(
    private movieService: MovieService,
    private alertService: AlertService) { }

  processKeyup(query: string){
    if(!query){
      this.movies = [];
    }
    this.movieService.searchMovie(query)
      .subscribe(
        (response: Movie[]) => {
          this.movies = response;
        },
        (error) => {
          this.alertService.error(error);
        }
      );
  }
}
