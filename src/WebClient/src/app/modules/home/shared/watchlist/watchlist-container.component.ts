import {Component, Input} from '@angular/core';
import {Watchlist} from "../../../../core/models/watchlist";

@Component({
  selector: 'watchlist-container',
  templateUrl: './watchlist-container.component.html',
  styleUrls: ['./watchlist-container.component.scss']
})

export class WatchlistContainerComponent {
  @Input() watchlist: Watchlist | undefined;
}
