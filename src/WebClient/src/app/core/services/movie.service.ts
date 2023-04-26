import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';
import { Movie } from '../models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private httpClient: HttpClient) { }

  searchMovie(query: string): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>(`movies/search/${query}`);
  }

  getPopularMovies(): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>('popular');
  }

  getTopRated(): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>('top-rated')
  }

  getUpcoming(): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>('upcoming')
  }

  getMovie(id: number): Observable<Movie>{
    return this.httpClient.get<Movie>(`id`)
  }
}
