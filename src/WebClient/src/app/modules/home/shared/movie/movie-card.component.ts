import {Component, Input} from '@angular/core';
import {Movie} from "../../../../core/models/movie";

@Component({
  selector: 'movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.scss']
})
export class MovieCardComponent {
  @Input() movie: Movie = {
    adult: false,
    originalTitle: "",
    releaseDate: "",
    title: "",
    video: false,
    backdropPath: "",
    genreIds: [],
    originalLanguage: "",
    overview: "",
    posterPath: "",
    voteAverage: 0,
    voteCount: 0,
    id: 0,
    mediaType: 0,
    popularity: 0
  };

  handleImageError(event: any) {
    event.target.src = "../../../../../assets/img/placeholder.jpg";
  }

  ngOnInit() {
  }
}
