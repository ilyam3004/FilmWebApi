import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { MoviesComponent } from './movies/movies.component';
import { WatchlistsComponent } from './watchlists/watchlists.component';
import { SearchComponent } from './search/search.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LayoutComponent } from './layout/layout.component';
import { SuggestionsComponent } from './suggestions/suggestions.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    MoviesComponent,
    WatchlistsComponent,
    SearchComponent,
    SidebarComponent,
    LayoutComponent,
    SuggestionsComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class HomeModule { }
