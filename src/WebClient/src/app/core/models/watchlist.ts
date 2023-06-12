import {Movie, MovieResponse} from './movie';

export interface Watchlist{
    id: string;
    name: string;
    moviesCount: number;
    dateTimeOfCreating: string;
    movies: MovieResponse[];
}
