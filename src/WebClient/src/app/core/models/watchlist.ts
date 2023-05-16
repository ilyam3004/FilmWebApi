import {Movie} from './movie';

export interface Watchlist{
    id: string;
    name: string;
    moviesCount: number;
    movies: Movie[];
}