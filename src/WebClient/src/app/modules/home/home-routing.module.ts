import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {WatchlistsComponent} from "./watchlists/watchlists.component";
import {SearchComponent} from "./search/search.component";
import {LayoutComponent} from "./layout/layout.component";
import {MoviesComponent} from "./movies/movies.component";
import {SuggestionsComponent} from "./suggestions/suggestions.component";

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: 'movies', component: MoviesComponent},
      { path: 'watchlists', component: WatchlistsComponent},
      { path: 'search', component: SearchComponent},
      { path: 'suggestions', component: SuggestionsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class HomeRoutingModule { }
