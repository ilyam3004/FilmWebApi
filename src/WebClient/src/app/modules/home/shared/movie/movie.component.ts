import {Component, Input} from '@angular/core';
import {Movie} from "../../../../core/models/movie";

@Component({
  selector: 'movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent {
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

  imageError = false;

  handleImageError(event: any) {
    this.imageError = true;
  }

  ngOnInit() {
  }
}