import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WatchlistsComponent } from "./watchlists/watchlists.component";
import { SearchComponent } from "./search/search.component";
import { LayoutComponent } from "./layout/layout.component";
import { MoviesComponent } from "./movies/movies.component";
import { RecommendationsComponent } from "./recommendations/recommendations.component";
import { DetailComponent } from "./detail/detail.component";

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: 'movies', component: MoviesComponent },
      { path: 'watchlists', component: WatchlistsComponent },
      { path: 'search', component: SearchComponent },
      { path: 'suggestions', component: RecommendationsComponent },
      { path: 'details', component: DetailComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class HomeRoutingModule { }
