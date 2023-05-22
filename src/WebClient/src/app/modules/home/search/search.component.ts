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

    // this.movies = [
    //   {
    //     "adult": false,
    //     "originalTitle": "Spider-Man: The Venom Saga",
    //     "releaseDate": "1994-06-07T00:00:00",
    //     "title": "Spider-Man: The Venom Saga",
    //     "video": true,
    //     "backdropPath": "https://image.tmdb.org/t/p/original",
    //     "genreIds": [
    //       16
    //     ],
    //     "originalLanguage": "en",
    //     "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
    //     "posterPath": "",
    //     "voteAverage": 6.7,
    //     "voteCount": 73,
    //     "id": 50410,
    //     "mediaType": 1,
    //     "popularity": 7.123
    //   },
    //   {
    //     "adult": false,
    //     "originalTitle": "Spider-Man: The Venom Saga",
    //     "releaseDate": "1994-06-07T00:00:00",
    //     "title": "Spider-Man: The Venom Saga",
    //     "video": true,
    //     "backdropPath": "https://image.tmdb.org/t/p/original",
    //     "genreIds": [
    //       16
    //     ],
    //     "originalLanguage": "en",
    //     "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
    //     "posterPath": "https://image.tmdb.org/t/p/original/ilmsQLtthtcD8EU1k25cp0xFQ9a.jpg",
    //     "voteAverage": 6.7,
    //     "voteCount": 73,
    //     "id": 50410,
    //     "mediaType": 1,
    //     "popularity": 7.123
    //   },]
  }
}
