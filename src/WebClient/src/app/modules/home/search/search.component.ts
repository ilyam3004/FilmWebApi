import {Component, OnInit} from '@angular/core';
import {MovieService} from "../../../core/services/movie.service";
import {Movie} from "../../../core/models/movie";
import {AlertService} from 'src/app/shared/services/alert.service';

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
    private alertService: AlertService) {
  }

  ngOnInit() {
  }

  onSubmit() {
    console.log(this.inputValue)
    if (!this.inputValue) {
      this.movies = [];
      return;
    }
    this.loading = true;

    //--------develop---------
    // this.loading = false
    // this.movies = [
    //   {
    //     adult: false,
    //     backdropPath: "/oBmgFY9ZMzMfNYfhbsUYvinDpee.jpg",
    //     genreIds: [],
    //     id: 1017678,
    //     originalLanguage: "es",
    //     originalTitle: "31 Minutos: Cuarentena 31 y Querido Diario (La Serie Completa)",
    //     overview: "The star journalist of 31 Minutos, Juan Carlos Bodoque has to lock himself in his house due to the COVID-19 pandemic",
    //     popularity: 0.6,
    //     posterPath: "/3wEAd6bnz2EAu7Kt2k3e5HOslpx.jpg",
    //     releaseDate: "2021-11-12",
    //     title: "31 Minutos: Cuarentena 31 & Querido Diario (The Complete Series)",
    //     video: false,
    //     voteAverage: 0.0,
    //     voteCount: 0,
    //     mediaType: 1
    //   },
    //   {
    //     adult: false,
    //     backdropPath: "/oBmgFY9ZMzMfNYfhbsUYvinDpee.jpg",
    //     genreIds: [],
    //     id: 1017678,
    //     originalLanguage: "es",
    //     originalTitle: "31 Minutos: Cuarentena 31 y Querido Diario (La Serie Completa)",
    //     overview: "The star journalist of 31 Minutos, Juan Carlos Bodoque has to lock himself in his house due to the COVID-19 pandemic",
    //     popularity: 0.6,
    //     posterPath: "/3wEAd6bnz2EAu7Kt2k3e5HOslpx.jpg",
    //     releaseDate: "2021-11-12",
    //     title: "31 Minutos: Cuarentena 31 & Querido Diario (The Complete Series)",
    //     video: false,
    //     voteAverage: 0.0,
    //     voteCount: 0,
    //     mediaType: 1
    //   }
    // ];
    //--------develop---------


    this.movieService.searchMovie(this.inputValue)
      .subscribe((response: Movie[]) => {
          this.movies = response;
          this.loading = false;
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
