import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from 'rxjs';
import {Watchlist} from "../models/watchlist";
import {Recommendation} from "../models/recommendation";

@Injectable({
    providedIn: 'root'
})

export class WatchlistService {
    constructor(private httpClient: HttpClient) { }

    addMovieToWatchlist(watchlistId: string, movieId: number): Observable<Watchlist> {
        return this.httpClient
            .post<Watchlist>(`watchlists/${watchlistId}/movie/${movieId}`, null);
    }

    removeMovieFromWatchlist(watchlistId: string, movieId: number): Observable<Watchlist> {
        return this.httpClient
            .delete<Watchlist>(`watchlists/${watchlistId}/movie/${movieId}`);
    }

    removeWatchlist(watchlistId: string){
      return this.httpClient.delete(`watchlists/${watchlistId}`)
    }

    getWatchlists(): Observable<Watchlist[]> {
        return this.httpClient
            .get<Watchlist[]>("watchlists")
    }

    createWatchlist(name: string): Observable<Watchlist> {
        return this.httpClient
            .post<Watchlist>(`watchlists/${name}`, null)
    }

    getRecommendations(): Observable<Recommendation[]> {
      return this.httpClient
        .get<Recommendation[]>("watchlists/recommendations");
    }
}
