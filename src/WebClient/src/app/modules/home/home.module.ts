import {NgModule} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {HomeRoutingModule} from './home-routing.module';
import {MoviesComponent} from './movies/movies.component';
import {WatchlistsComponent} from './watchlists/watchlists.component';
import {SearchComponent} from './search/search.component';
import {NavbarComponent} from './navbar/navbar.component';
import {LayoutComponent} from './layout/layout.component';
import {SuggestionsComponent} from './suggestions/suggestions.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MovieComponent} from './shared/movie/movie.component';
import {DetailComponent} from './detail/detail.component';
import {NgxYoutubePlayerModule} from "ngx-youtube-player";
import {NgbCarousel, NgbCarouselModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';
import {TableComponent} from './shared/table/table.component';
import {MovieDataTableComponent} from './shared/movie-data-table/movie-data-table.component';
import {CarouselComponent} from './shared/carousel/carousel.component'; 

@NgModule({
    declarations: [
        MoviesComponent,
        WatchlistsComponent,
        SearchComponent,
        NavbarComponent,
        LayoutComponent,
        SuggestionsComponent,
        MovieComponent,
        DetailComponent,
        TableComponent,
        MovieDataTableComponent,
        CarouselComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        NgbDropdownModule,
        FormsModule,
        ReactiveFormsModule,
        NgbCarouselModule,
        NgxYoutubePlayerModule.forRoot(),
    ]
})
export class HomeModule {
}
