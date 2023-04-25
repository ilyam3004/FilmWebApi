import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment.development";
import { Observable } from 'rxjs';
import { Movie } from '../models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private httpClient: HttpClient) { }

  searchMovie(query: string): Observable<Movie[]>{
    return this.httpClient.get<Movie[]>(`
        ${environment.apiBaseUrl}/movies/search/${query}`);
  }
}
