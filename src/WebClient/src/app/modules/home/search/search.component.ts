import { Component } from '@angular/core';
import {MovieService} from "../../../core/services/movie.service";
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  movies: any;
  form = this.formBuilder.group({
    query: ''
  });

  constructor(
    private movieService: MovieService,
    private formBuilder: FormBuilder,) { }

  onSubmit():void {
    if (this.form.value.query)
    this.movieService.searchMovie(this.form.value.query)
      .subscribe(response => {
        this.movies = response;
      });
    console.log(this.movies);
  }
}
