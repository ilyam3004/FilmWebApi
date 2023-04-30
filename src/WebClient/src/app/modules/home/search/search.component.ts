import {Component, OnInit} from '@angular/core';
import {MovieService} from "../../../core/services/movie.service";
import {Movie} from "../../../core/models/movie";
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  inputValue: string = "";
  loading: boolean = false;
  movies: Movie[] = [];

  constructor(
    private movieService: MovieService,
    private alertService: AlertService) { }

  ngOnInit() { }

  onSubmit() {
    if(!this.inputValue){
      this.movies = [];
      return;
    }
    this.loading = true;

    setTimeout(() => {
      this.loading = false;
    })

    this.movies = [
      {
        "adult": false,
        "originalTitle": "The Super Mario Bros. Movie",
        "releaseDate": "2023-04-05T00:00:00",
        "title": "The Super Mario Bros. Movie",
        "video": false,
        "backdropPath": "/9n2tJBplPbgR2ca05hS5CKXwP2c.jpg",
        "genreIds": [
          16,
          12,
          10751,
          14,
          35
        ],
        "originalLanguage": "en",
        "overview": "While working underground to fix a water main, Brooklyn plumbers—and brothers—Mario and Luigi are transported down a mysterious pipe and wander into a magical new world. But when the brothers are separated, Mario embarks on an epic quest to find Luigi.",
        "posterPath": "/qNBAXBIQlnOThrVvA6mA2B5ggV6.jpg",
        "voteAverage": 7.515,
        "voteCount": 1543,
        "id": 502356,
        "mediaType": 1,
        "popularity": 7475.651
      },
      {
        "adult": false,
        "originalTitle": "劇場版「鬼滅の刃」無限列車編",
        "releaseDate": "2020-10-16T00:00:00",
        "title": "Demon Slayer -Kimetsu no Yaiba- The Movie: Mugen Train",
        "video": false,
        "backdropPath": "/xPpXYnCWfjkt3zzE0dpCNME1pXF.jpg",
        "genreIds": [
          16,
          28,
          12,
          14,
          53
        ],
        "originalLanguage": "ja",
        "overview": "Tanjiro Kamado, joined with Inosuke Hashibira, a boy raised by boars who wears a boar's head, and Zenitsu Agatsuma, a scared boy who reveals his true power when he sleeps, boards the Infinity Train on a new mission with the Fire Hashira, Kyojuro Rengoku, to defeat a demon who has been tormenting the people and killing the demon slayers who oppose it!",
        "posterPath": "/h8Rb9gBr48ODIwYUttZNYeMWeUU.jpg",
        "voteAverage": 8.25,
        "voteCount": 3061,
        "id": 635302,
        "mediaType": 1,
        "popularity": 279.848
      },
      {
        "adult": false,
        "originalTitle": "THE LAST -NARUTO THE MOVIE-",
        "releaseDate": "2014-12-06T00:00:00",
        "title": "The Last: Naruto the Movie",
        "video": false,
        "backdropPath": "/l8ubUlfzlB5R2j9cJ3CN7tj0gmd.jpg",
        "genreIds": [
          28,
          10749,
          16
        ],
        "originalLanguage": "ja",
        "overview": "Two years after the events of the Fourth Great Ninja War, the moon that Hagoromo Otsutsuki created long ago to seal away the Gedo Statue begins to descend towards the world, threatening to become a meteor that would destroy everything on impact. Amidst this crisis, a direct descendant of Kaguya Otsutsuki named Toneri Otsutsuki attempts to kidnap Hinata Hyuga but ends up abducting her younger sister Hanabi. Naruto and his allies now mount a rescue mission before finding themselves embroiled in a final battle to decide the fate of everything.",
        "posterPath": "/bAQ8O5Uw6FedtlCbJTutenzPVKd.jpg",
        "voteAverage": 7.753,
        "voteCount": 1655,
        "id": 317442,
        "mediaType": 1,
        "popularity": 80.593
      },
      {
        "adult": false,
        "originalTitle": "Mo",
        "releaseDate": "2019-06-07T00:00:00",
        "title": "MO",
        "video": false,
        "backdropPath": "/qPXOJKgZTlxUR6kRTBI5pb2GS2h.jpg",
        "genreIds": [
          18
        ],
        "originalLanguage": "ro",
        "overview": "Two college students and best girlfriends get caught cheating on an exam, so their professor takes his revenge on them in the worst possible way.",
        "posterPath": "/gGSYd8uzcgzfjEAH86VM2Yss3oY.jpg",
        "voteAverage": 6,
        "voteCount": 13,
        "id": 607926,
        "mediaType": 1,
        "popularity": 2.22
      },
      {
        "adult": false,
        "originalTitle": "BORUTO -NARUTO THE MOVIE-",
        "releaseDate": "2015-08-07T00:00:00",
        "title": "Boruto: Naruto the Movie",
        "video": false,
        "backdropPath": "/8kQtBcax6zqiwFhDRS2mmPvLf8.jpg",
        "genreIds": [
          16,
          28,
          35
        ],
        "originalLanguage": "ja",
        "overview": "The spirited Boruto Uzumaki, son of Seventh Hokage Naruto, is a skilled ninja who possesses the same brashness and passion his father once had. However, the constant absence of his father, who is busy with his Hokage duties, puts a damper on Boruto's fire. He ends up meeting his father's friend Sasuke, and requests to become... his apprentice!? The curtain on the story of the new generation rises!",
        "posterPath": "/1k6iwC4KaPvTBt1JuaqXy3noZRY.jpg",
        "voteAverage": 7.584,
        "voteCount": 1275,
        "id": 347201,
        "mediaType": 1,
        "popularity": 66.311
      },
    ];
  }
}
