import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { MoviesComponent } from './movies/movies.component';
import WatchlistsComponent from './watchlists/watchlists.component';
import { SearchComponent } from './search/search.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LayoutComponent } from './layout/layout.component';
import { SuggestionsComponent } from './suggestions/suggestions.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MovieCardComponent } from './shared/movie/movie-card.component';
import { DetailComponent } from './detail/detail.component';
import { NgxYoutubePlayerModule } from "ngx-youtube-player";
import { NgbCarouselModule, NgbDatepickerModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { TableComponent } from './shared/table/table.component';
import { MovieDataTableComponent } from './shared/movie-data-table/movie-data-table.component';
import { CarouselComponent } from './shared/carousel/carousel.component';
import { SliderComponent } from './shared/slider/slider.component';
import { ModalComponent } from './shared/modal/modal.component';
import { WatchlistComponent } from './shared/watchlist/watchlist.component';

@NgModule({
    declarations: [
        MoviesComponent,
        WatchlistsComponent,
        SearchComponent,
        NavbarComponent,
        LayoutComponent,
        SuggestionsComponent,
        MovieCardComponent,
        DetailComponent,
        TableComponent,
        MovieDataTableComponent,
        CarouselComponent,
        SliderComponent,
        ModalComponent,
        WatchlistComponent
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
