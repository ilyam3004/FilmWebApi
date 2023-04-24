import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment.development";

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private httpClient: HttpClient) { }

  searchMovie(query: string){
    return this.httpClient.get(`
        ${environment.apiBaseUrl}/movies/search/${query}`);
  }
}
