import {Component, OnInit} from '@angular/core';
import {Recommendation} from "../../../core/models/recommendation";
import {WatchlistService} from "../../../core/services/watchlist.service";
import {AlertService} from "../../../shared/services/alert.service";
import {DatePipe} from "@angular/common";


@Component({
  selector: 'app-suggestions',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.scss']
})
export class RecommendationsComponent implements OnInit {
  recommendations: Recommendation[] = [];
  recommendationsNotFound: boolean = false;
  isLoading: boolean = false;

  constructor(private watchlistService: WatchlistService,
              private alertService: AlertService,
              private datePipe: DatePipe) {
  }

  ngOnInit() {
    this.isLoading = false;
    //this.getRecommendations();
    this.recommendations = [
      {
        watchlistName: "WatchlistName",
        movies: [{
          "adult": false,
          "originalTitle": "Spider-Man: The Venom Saga",
          "releaseDate": "1994-06-07T00:00:00",
          "title": "Spider-Man: The Venom Saga",
          "video": true,
          "backdropPath": "https://image.tmdb.org/t/p/original",
          "genreIds": [
            16
          ],
          "originalLanguage": "en",
          "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
          "posterPath": "",
          "voteAverage": 6.7,
          "voteCount": 73,
          "id": 50410,
          "mediaType": 1,
          "popularity": 7.123
        },
          {
            "adult": false,
            "originalTitle": "Spider-Man: The Venom Saga",
            "releaseDate": "1994-06-07T00:00:00",
            "title": "Spider-Man: The Venom Saga",
            "video": true,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [
              16
            ],
            "originalLanguage": "en",
            "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
            "posterPath": "https://image.tmdb.org/t/p/original/ilmsQLtthtcD8EU1k25cp0xFQ9a.jpg",
            "voteAverage": 6.7,
            "voteCount": 73,
            "id": 50410,
            "mediaType": 1,
            "popularity": 7.123
          }]
      },
      {
        watchlistName: "Watchlist",
        movies: [
          {
            "adult": false,
            "originalTitle": "Spider-Man: The Venom Saga",
            "releaseDate": "1994-06-07T00:00:00",
            "title": "Spider-Man: The Venom Saga",
            "video": true,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [
              16
            ],
            "originalLanguage": "en",
            "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
            "posterPath": "",
            "voteAverage": 6.7,
            "voteCount": 73,
            "id": 50410,
            "mediaType": 1,
            "popularity": 7.123
          },
          {
            "adult": false,
            "originalTitle": "Spider-Man: The Venom Saga",
            "releaseDate": "1994-06-07T00:00:00",
            "title": "Spider-Man: The Venom Saga",
            "video": true,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [
              16
            ],
            "originalLanguage": "en",
            "overview": "A space-shuttle crash-landing puts the famous web-slinger Spider-Man in contact with a living alien substance that bonds to his suit and enhances his super-powers. Unfortunately, the alien substance begins to change him and he feels the pull of evil, so discards the suit. The evil attaches itself to another host leading to an epic confrontation between good and evil.",
            "posterPath": "https://image.tmdb.org/t/p/original/ilmsQLtthtcD8EU1k25cp0xFQ9a.jpg",
            "voteAverage": 6.7,
            "voteCount": 73,
            "id": 50410,
            "mediaType": 1,
            "popularity": 7.123
          },]
      }]
  }

  getRecommendations() {
    this.watchlistService.getRecommendations()
      .subscribe((response: Recommendation[]) => {
          this.recommendations = response;
          this.isLoading = false;
          if (this.recommendations.length == 0) {
            this.recommendationsNotFound = true;
          }
        },
        (error) => {
          this.alertService.error(error);
        });
  }

  getFormattedDate(date: string | null): string{
      return this.datePipe
        .transform(date, 'MMM d, y') || "-";
  }
}
