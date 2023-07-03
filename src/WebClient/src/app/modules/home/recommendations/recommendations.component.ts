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
    this.isLoading = true;
    this.getRecommendations();
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
          this.isLoading = false;
          if(error === "Watchlists not found"){
            this.recommendationsNotFound = true;
            return;
          }
          this.alertService.error(error);
        });
  }

  getFormattedDate(date: string | null): string {
    return this.datePipe
      .transform(date, 'MMM d, y') || "-";
  }
}
