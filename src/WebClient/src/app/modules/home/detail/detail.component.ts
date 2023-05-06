import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Movie} from "../../../core/models/movie";
import {MovieService} from "../../../core/services/movie.service";
import {AlertService} from "../../../shared/services/alert.service";

@Component({
  selector: 'movie-details',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})

export class DetailComponent implements OnInit {
  movie: Movie | null = null;
  loading: boolean = false;
  id: number = 0;

  constructor(private route: ActivatedRoute,
              private movieService: MovieService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.id = params.id
      });
    // this.movie = {
    //   adult: false,
    //   backdropPath: "/oBmgFY9ZMzMfNYfhbsUYvinDpee.jpg",
    //   genreIds: [],
    //   id: 1017678,
    //   originalLanguage: "es",
    //   originalTitle: "31 Minutos: Cuarentena 31 y Querido Diario (La Serie Completa)",
    //   overview: "The star journalist of 31 Minutos, Juan Carlos Bodoque has to lock himself in his house due to the COVID-19 pandemic",
    //   popularity: 0.6,
    //   posterPath: "/3wEAd6bnz2EAu7Kt2k3e5HOslpx.jpg",
    //   releaseDate: "2021-11-12",
    //   title: "31 Minutos: Cuarentena 31 & Querido Diario (The Complete Series)",
    //   video: false,
    //   voteAverage: 0.0,
    //   voteCount: 0,
    //   mediaType: 1
    // }
    this.getMovieData();
  }

  getMovieData(){
    this.loading = true;
    this.movieService.getMovie(this.id)
      .subscribe((response: Movie) => {
          this.movie = response;
          this.loading = false;
        },
        (error) => {
          this.alertService.error(error);
          this.loading = false;
        });
    this.loading = false;
  }

  handleImageError(event: any){
    event.target.src = "./assets/img/placeholder.jpg"
  }
}
