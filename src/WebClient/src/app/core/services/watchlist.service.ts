import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observer} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WatchlistService {
  constructor(private httpClient: HttpClient) { }

  addMovieToWatchlist(watchlistId: string, movieId: number) {

  }
}
