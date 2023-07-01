import {Movie} from "./movie";

export interface Recommendation {
  watchlistName: string;
  movies: Movie[]
}
