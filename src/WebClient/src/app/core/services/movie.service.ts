import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';
import { Movie, MovieDetails } from '../models/movie';

@Injectable({
  providedIn: 'root'
})

export class MovieService {
  constructor(private httpClient: HttpClient) { }

  searchMovie(query: string): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>(`movies/search/${query}`);
  }

  getPopularMovies(): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>('movies/popular');
  }

  getTopRatedMovies(): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>('movies/top-rated')
  }
  
  getUpcomingMovies(): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>('movies/upcoming')
  }

  getMovieDetails(id: number): Observable<MovieDetails>{
    return this.httpClient.get<MovieDetails>(`movies/${id}`)
  }
}
