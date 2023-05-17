import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from 'rxjs';
import {Watchlist} from "../models/watchlist";

@Injectable({
    providedIn: 'root'
})

export class WatchlistService {
    constructor(private httpClient: HttpClient) {
    }

    addMovieToWatchlist(watchlistId: string, movieId: number): Observable<Watchlist> {
        return this.httpClient
            .post<Watchlist>(`watchlists/${watchlistId}/movies/${movieId}`, null);
    }

    removeMovieFromWatchlist(watchlistId: string, movieId: number): Observable<Watchlist> {
        return this.httpClient
            .delete<Watchlist>(`watchlists/${watchlistId}/movies/${movieId}`);
    }

    getWatchlists(): Observable<Watchlist[]> {
        return this.httpClient
            .get<Watchlist[]>("watchlists")
    }

    createWatchlist(name: string): Observable<Watchlist> {
        return this.httpClient
            .post<Watchlist>(`watchlists/${name}`, null)
    }
}