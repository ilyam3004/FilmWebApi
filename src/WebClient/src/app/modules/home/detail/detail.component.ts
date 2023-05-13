import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { MovieDetails, Movie } from "../../../core/models/movie";
import { MovieService } from "../../../core/services/movie.service";
import { AlertService } from "../../../shared/services/alert.service";

@Component({
  selector: 'movie-details',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})

export class DetailComponent implements OnInit {
  movie: MovieDetails | undefined;
  loading: boolean = false;
  movieId: number = 0;
  player: YT.Player | undefined;

  constructor(private route: ActivatedRoute,
    private movieService: MovieService,
    private alertService: AlertService) { }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.movieId = params.id
      });

      // this.movie = {
      //   adult: false,
      //   backdropPath: "https://image.tmdb.org/t/p/original/iJQIbOPm81fPEGKt5BPuZmfnA54.jpg",
      //   budget: 100000000,
      //   genres: [
      //   {
      //     id: 16,
      //     name: "Animation"
      //   },
      //   {
      //     id: 12,
      //     name: "Adventure"
      //   },
      //   {
      //     id: 10751,
      //     name: "Family"
      //   },
      //   {
      //     id: 14,
      //     name: "Fantasy"
      //   },
      //   {
      //     id: 35,
      //     name: "Comedy"
      //   }
      // ],
      //   homepage: "https://www.thesupermariobros.movie",
      //   id: 502356,
      //   imdbId: "tt6718170",
      //   originalLanguage: "en",
      //   originalTitle: "The Super Mario Bros. Movie",
      //   overview: "While working underground to fix a water main, Brooklyn plumbers—and brothers—Mario and Luigi are transported down a mysterious pipe and wander into a magical new world. But when the brothers are separated, Mario embarks on an epic quest to find Luigi.",
      //   popularity: 3935.55,
      //   posterPath: "https://image.tmdb.org/t/p/original/qNBAXBIQlnOThrVvA6mA2B5ggV6.jpg",
      //   productionCompanies: [
      //   {
      //     id: 33,
      //     name: "Universal Pictures",
      //     logoPath: "/8lvHyhjr8oUKOOy2dKXoALWKdp0.png",
      //     originCountry: "US"
      //   },
      //   {
      //     id: 6704,
      //     name: "Illumination",
      //     logoPath: "/fOG2oY4m1YuYTQh4bMqqZkmgOAI.png",
      //     originCountry: "US"
      //   },
      //   {
      //     id: 12288,
      //     name: "Nintendo",
      //     logoPath: "/e4dQAqZD374H5EuM0W1ljEBWTKy.png",
      //     originCountry: "JP"
      //   }
      // ],
      //   productionCountries: [
      //   {
      //     iso_3166_1: "JP",
      //     name: "Japan"
      //   },
      //   {
      //     iso_3166_1: "US",
      //     name: "United States of America"
      //   }
      // ],
      //   releaseDate: "2023-04-05T00:00:00",
      //   revenue: 1121048165,
      //   runtime: 92,
      //   similar: null,
      //   recommendations: null,
      //   spokenLanguages: [
      //   {
      //     iso_639_1: "en",
      //     name: "English"
      //   }
      // ],
      //   status: "Released",
      //   tagline: "",
      //   title: "The Super Mario Bros. Movie",
      //   video: false,
      //   videos: null,
      //   voteAverage: 7.498,
      //   voteCount: 1870
      // }
    this.getMovieData();
  }

  savePlayer(player: YT.Player) {
    this.player = player;
  }

  onStateChange(event: any) {
    console.log('player state', event.data);
  }

  getMovieData(){
    this.loading = true;
    this.movieService.getMovieDetails(this.movieId)
      .subscribe((response: MovieDetails) => {
          console.log(response);
          this.movie = response;
          this.loading = false;
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        });
    this.loading = false;
  }

  handleImageError(event: any) {
    event.target.src = "./assets/img/placeholder.jpg"
  }
}
