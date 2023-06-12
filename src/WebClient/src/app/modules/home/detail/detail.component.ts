import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MovieDetails} from "../../../core/models/movie";
import {MovieService} from "../../../core/services/movie.service";
import {AlertService} from "../../../shared/services/alert.service";
import {Watchlist} from "../../../core/models/watchlist";
import {WatchlistService} from "../../../core/services/watchlist.service";


@Component({
  selector: 'movie-details',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})

export class DetailComponent implements OnInit {
  movie: MovieDetails | undefined;
  loading: boolean = false;
  movieId: number = 0;
  player: YT.Player | undefined;
  addedToWatchlist: boolean = true;
  watchlists: Watchlist[] | undefined;

  constructor(private route: ActivatedRoute,
              private movieService: MovieService,
              private alertService: AlertService,
              private watchlistService: WatchlistService) {
  }


  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.movieId = params.id
      });


    // this.watchlistService.getWatchlists()
    //     .pipe(first())
    //     .subscribe({
    //         next: (response: Watchlist[]) => {
    //             this.watchlists = response;
    //         },
    //         error: error => {
    //             this.alertService.error(error);
    //         }
    //     });
    //
    // this.getMovieData();

    this.watchlists = [
      {
        id: "laskdsalkdj",
        name: "watchlist",
        moviesCount: 2,
        movies: [],
        dateTimeOfCreating: "4 days ago"
      },
      {
        id: "laskdsalkdj",
        name: "watchlist1",
        moviesCount: 2,
        movies: [],
        dateTimeOfCreating: "4 days ago"
      },
      {
        id: "laskdsalkdj",
        name: "watchlist2",
        moviesCount: 2,
        movies: [],
        dateTimeOfCreating: "4 days ago"
      },
    ]

    this.movie = {
      "adult": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/xcXALwBjdHIjrESpGVhghqj8fGT.jpg",
      "budget": 100000000,
      "genres": [
        {
          "id": 18,
          "name": "Drama"
        },
        {
          "id": 36,
          "name": "History"
        }
      ],
      "homepage": "http://www.oppenheimermovie.com",
      "id": 872585,
      "imdbId": "tt15398776",
      "originalLanguage": "en",
      "originalTitle": "Oppenheimer",
      "overview": "The story of J. Robert Oppenheimer’s role in the development of the atomic bomb during World War II.",
      "popularity": 36.303,
      "posterPath": "https://image.tmdb.org/t/p/original/k76lCV5thtgYFoSqPHz1JvyaKCw.jpg",
      "productionCompanies": [
        {
          "id": 9996,
          "name": "Syncopy",
          "logoPath": "https://image.tmdb.org/t/p/original/3tvBqYsBhxWeHlu62SIJ1el93O7.png",
          "originCountry": "GB"
        },
        {
          "id": 33,
          "name": "Universal Pictures",
          "logoPath": "https://image.tmdb.org/t/p/original/8lvHyhjr8oUKOOy2dKXoALWKdp0.png",
          "originCountry": "US"
        },
        {
          "id": 507,
          "name": "Atlas Entertainment",
          "logoPath": "https://image.tmdb.org/t/p/original/z7H707qUWigbjHnJDMfj6QITEpb.png",
          "originCountry": "US"
        }
      ],
      "productionCountries": [
        {
          "iso_3166_1": "GB",
          "name": "United Kingdom"
        },
        {
          "iso_3166_1": "US",
          "name": "United States of America"
        }
      ],
      "releaseDate": "2023-07-19T00:00:00",
      "revenue": 0,
      "runtime": 0,
      "similar": {
        "page": 1,
        "results": [
          {
            "adult": false,
            "originalTitle": "De Behandeling",
            "releaseDate": "2014-01-29T00:00:00",
            "title": "The Treatment",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/oyoHGvtPdRa7BCXXQcHLzB6LNO4.jpg",
            "genreIds": [
              9648,
              53
            ],
            "originalLanguage": "nl",
            "overview": "Inspector Nick Cafmeyer seems to have it all - looks, brains and a successful career.  But a dark cloud hangs over his life: since the age of nine, he has been haunted by the unsolved disappearance of his younger brother, Bjorn. Plettinckx, a known sex offender, was questioned but quickly released. Plettinckx lives close by and takes fiendish pleasure in harassing Nick. Then, when a disturbing case comes to light involving a missing nine-year-old, Nick heads a massive search which turns into a relentless manhunt.",
            "posterPath": "https://image.tmdb.org/t/p/original/nzVhjAjEaugzs0CSmEO50W5mTcc.jpg",
            "voteAverage": 6.961,
            "voteCount": 165,
            "id": 251577,
            "mediaType": 1,
            "popularity": 6.283
          },
          {
            "adult": false,
            "originalTitle": "Simenon et l'affaire du cinéma",
            "releaseDate": "2022-03-27T00:00:00",
            "title": "Simenon et l'affaire du cinéma",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/4muwGYtDfGSxk9KKtznAzjsrk3V.jpg",
            "genreIds": [
              99
            ],
            "originalLanguage": "fr",
            "overview": "Why did Simenon, a novelist who contributed so much to the seventh art, like to say that he hated the cinema? Because he could never become a director? Because, claustrophobic, he was unable to lock himself in a projection room? Clearly, there is an affair between the writer and the cinema and Georges Simenon is the main protagonist. An investigation that is more than ever topical as Patrice Leconte has announced his plan to adapt an investigation by the famous Inspector Maigret.",
            "posterPath": "https://image.tmdb.org/t/p/original/hKQbMj6CnACu8KIyYNSvDn3EFGN.jpg",
            "voteAverage": 0,
            "voteCount": 0,
            "id": 955153,
            "mediaType": 1,
            "popularity": 0.6
          },
          {
            "adult": false,
            "originalTitle": "Pod Mocnym Aniołem",
            "releaseDate": "2014-01-17T00:00:00",
            "title": "The Mighty Angel",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/bmgx81jUt3OU811T0ok9a8CnVyl.jpg",
            "genreIds": [
              18
            ],
            "originalLanguage": "pl",
            "overview": "A shocking story about addiction and attempts to overcome it. The script is based on Jerzy Plich’s excellent novel “The Mighty Angel” (alternative title: \"The Strong Angel Inn\").  Jerzy (Robert Więckiewicz) is a writer and a heavy drinker. We meet him at the point when he believes that he can beat his addiction. He falls in love with a young girl (Julia Kijowska) and finally feels that he has got the person and the reason to live for. But soon he yields to his addiction.",
            "posterPath": "https://image.tmdb.org/t/p/original/9I9U86mz0Riyy6H1HdfR10IIvim.jpg",
            "voteAverage": 6.5,
            "voteCount": 42,
            "id": 251584,
            "mediaType": 1,
            "popularity": 3.948
          },
          {
            "adult": false,
            "originalTitle": "シュンマオ物語 タオタオ",
            "releaseDate": "1981-12-27T00:00:00",
            "title": "Xiongmao Monogatari TaoTao",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/wy6IBqoigVuiZFeYiRj6Zxq0tac.jpg",
            "genreIds": [
              16
            ],
            "originalLanguage": "zh",
            "overview": "The Story of Panda – Taotao, directed by Shuichi Nakahara and Tatsuo Shimamura, was the first animation produced as\r a Chinese-Japanese joint venture. Released in December, 1981, the film is about Taotao, a small panda from Sichuan, who\r was captured and sold to a European zoo, where he got into adventures with his animal friends.  The series includes a film in 1981 and a 52-episode series in 1983.",
            "posterPath": "https://image.tmdb.org/t/p/original/t5h6gOZ5cxuPGQUR1WTXDECEhDe.jpg",
            "voteAverage": 6.333,
            "voteCount": 3,
            "id": 251627,
            "mediaType": 1,
            "popularity": 0.6
          },
          {
            "adult": false,
            "originalTitle": "Dobrodružství v tajze",
            "releaseDate": "1962-01-14T00:00:00",
            "title": "Dobrodružství v tajze",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [],
            "originalLanguage": "cs",
            "overview": "",
            "posterPath": "https://image.tmdb.org/t/p/original",
            "voteAverage": 0,
            "voteCount": 0,
            "id": 955052,
            "mediaType": 1,
            "popularity": 0.6
          },
          {
            "adult": false,
            "originalTitle": "Рихард Зорге. Резидент, которому не верили",
            "releaseDate": "2009-03-28T00:00:00",
            "title": "Рихард Зорге. Резидент, которому не верили",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [
              99,
              10752,
              36
            ],
            "originalLanguage": "ru",
            "overview": "",
            "posterPath": "https://image.tmdb.org/t/p/original/jJRSQ6xQ9lmNk1xzKBqu7cxucvG.jpg",
            "voteAverage": 8,
            "voteCount": 1,
            "id": 597195,
            "mediaType": 1,
            "popularity": 0.875
          },
          {
            "adult": false,
            "originalTitle": "Ghost Army",
            "releaseDate": null,
            "title": "Ghost Army",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [
              10752,
              18
            ],
            "originalLanguage": "en",
            "overview": "The US military forms a squadron of unconventional recruits during World War II to trick the German army into thinking there were outposts and bases where there were only mannequins, props and inflatable tanks.",
            "posterPath": "https://image.tmdb.org/t/p/original/vP66N4WQJCj6XBUZ6KNEbqG2Jmw.jpg",
            "voteAverage": 0,
            "voteCount": 0,
            "id": 597205,
            "mediaType": 1,
            "popularity": 0.84
          },
          {
            "adult": false,
            "originalTitle": "Nightmare Alley",
            "releaseDate": "2021-12-02T00:00:00",
            "title": "Nightmare Alley",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/mqDnDhG5N6fn1H4MKQqr8E5wfeK.jpg",
            "genreIds": [
              53
            ],
            "originalLanguage": "en",
            "overview": "An ambitious carnival man with a talent for manipulating people with a few well-chosen words hooks up with a female psychologist who is even more dangerous than he is.",
            "posterPath": "https://image.tmdb.org/t/p/original/680klE0dIreQQOyWKFgNnCAJtws.jpg",
            "voteAverage": 7.018,
            "voteCount": 2378,
            "id": 597208,
            "mediaType": 1,
            "popularity": 37.793
          },
          {
            "adult": false,
            "originalTitle": "Кто убил Рихарда Зорге?. Рождение мифа",
            "releaseDate": "2006-02-12T00:00:00",
            "title": "Кто убил Рихарда Зорге?. Рождение мифа",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/qXZVNLDNTCGivWFNIJgDdU4I0KN.jpg",
            "genreIds": [
              10752,
              36,
              10770,
              99
            ],
            "originalLanguage": "ru",
            "overview": "",
            "posterPath": "https://image.tmdb.org/t/p/original/raJeIjvjTLgx5xXEtl62nzG3IWo.jpg",
            "voteAverage": 9,
            "voteCount": 1,
            "id": 597076,
            "mediaType": 1,
            "popularity": 1.372
          },
          {
            "adult": false,
            "originalTitle": "Andrea Camilleri - Il maestro senza regole",
            "releaseDate": "2014-09-06T00:00:00",
            "title": "Montalbano and Me: Andrea Camilleri",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original",
            "genreIds": [
              10770
            ],
            "originalLanguage": "it",
            "overview": "A profile of the author of the highly successful INSPECTOR MONTALBANO series, originally broadcast to mark his birthday.",
            "posterPath": "https://image.tmdb.org/t/p/original/vuKZncDvEkNhM1SyX0Vn8QJxF26.jpg",
            "voteAverage": 6.3,
            "voteCount": 3,
            "id": 424033,
            "mediaType": 1,
            "popularity": 0.685
          },
          {
            "adult": false,
            "originalTitle": "Christina Lindberg: The Original Eyepatch Wearing Butt Kicking Movie Babe",
            "releaseDate": "2015-10-02T00:00:00",
            "title": "Christina Lindberg: The Original Eyepatch Wearing Butt Kicking Movie Babe",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/44PvhDpD12NtsoCKDkvhQ3ehF8K.jpg",
            "genreIds": [
              99
            ],
            "originalLanguage": "en",
            "overview": "A look at the life and work of Christina Lindberg, the most famous Swedish model of the 1970s and star of exploitation cinema.",
            "posterPath": "https://image.tmdb.org/t/p/original/cdybpobAwhaMRByzY44VZZJbuCp.jpg",
            "voteAverage": 4,
            "voteCount": 1,
            "id": 424108,
            "mediaType": 1,
            "popularity": 0.626
          },
          {
            "adult": false,
            "originalTitle": "Victory at Sea",
            "releaseDate": "1954-08-02T00:00:00",
            "title": "Victory at Sea",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/iKpPmRTqGFsE6YNBvBuit1YMDXt.jpg",
            "genreIds": [
              99
            ],
            "originalLanguage": "en",
            "overview": "A feature-length, condensed version of the 1952 documentary TV series 'Victory at Sea'.",
            "posterPath": "https://image.tmdb.org/t/p/original/eXFyIIsuCB7EZ4hOidkizaAVoB8.jpg",
            "voteAverage": 0,
            "voteCount": 0,
            "id": 443948,
            "mediaType": 1,
            "popularity": 0.6
          },
          {
            "adult": false,
            "originalTitle": "Hangmen Also Die!",
            "releaseDate": "1943-04-15T00:00:00",
            "title": "Hangmen Also Die!",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/7rL5tbuSmtpKeLAKcBTqQwmXAGj.jpg",
            "genreIds": [
              18,
              53,
              10752
            ],
            "originalLanguage": "en",
            "overview": "During the Nazi occupation of Czechoslovakia, surgeon Dr. Franticek Svoboda, a Czech patriot, assassinates the brutal \"Hangman of Europe\", Reichsprotektor Reinhard Heydrich, and is wounded in the process. In his attempt to escape, he is helped by history professor Stephen Novotny and his daughter Mascha.",
            "posterPath": "https://image.tmdb.org/t/p/original/d9J86BpP7NLreJJ9VCbr1SDMRii.jpg",
            "voteAverage": 7,
            "voteCount": 98,
            "id": 22178,
            "mediaType": 1,
            "popularity": 6.007
          },
          {
            "adult": false,
            "originalTitle": "Gunfight at the O.K. Corral",
            "releaseDate": "1957-05-30T00:00:00",
            "title": "Gunfight at the O.K. Corral",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/n4Ec7koHqqFKz0fC7vIitNxxqwi.jpg",
            "genreIds": [
              37
            ],
            "originalLanguage": "en",
            "overview": "Lawman Wyatt Earp and outlaw Doc Holliday form an unlikely alliance which culminates in their participation in the legendary Gunfight at the O.K. Corral.",
            "posterPath": "https://image.tmdb.org/t/p/original/71AQUFbw49ALCNtKZ69flFmbRhF.jpg",
            "voteAverage": 7.029,
            "voteCount": 278,
            "id": 22201,
            "mediaType": 1,
            "popularity": 13.239
          },
          {
            "adult": false,
            "originalTitle": "An Awfully Big Adventure",
            "releaseDate": "1995-07-21T00:00:00",
            "title": "An Awfully Big Adventure",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/jCMX5qTsYKxCHYBywCH8vzATLfU.jpg",
            "genreIds": [
              10749,
              18,
              35
            ],
            "originalLanguage": "en",
            "overview": "Liverpool. 1947. Right after World War II, a star struck naive teenage girl joins a shabby theatre troupe in Liverpool. During a winter production of Peter Pan, the play quickly turns into a dark metaphor for youth as she becomes drawn into a web of sexual politics and intrigue and learns about the grown-up world of the theater.",
            "posterPath": "https://image.tmdb.org/t/p/original/tfXBuRBMbyu9IF9KCFz3kXeWQDT.jpg",
            "voteAverage": 6.42,
            "voteCount": 40,
            "id": 22279,
            "mediaType": 1,
            "popularity": 5.999
          },
          {
            "adult": false,
            "originalTitle": "In Too Deep",
            "releaseDate": "1999-08-25T00:00:00",
            "title": "In Too Deep",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/vBPR3urSuYheuF8FjY5UZiPzqgV.jpg",
            "genreIds": [
              18,
              28,
              53,
              80
            ],
            "originalLanguage": "en",
            "overview": "Drug lord Dwayne Gittens rules Cincinnati with an iron fist. No wonder he's known as \"God\" on the streets. Determined to break Gittens' stranglehold on the city is undercover cop Jeffrey Cole. But as Cole takes on an assumed identity to penetrate Gittens' criminal empire, he makes a disturbing discovery -- he kind of likes being a gangster.",
            "posterPath": "https://image.tmdb.org/t/p/original/mYteXffai9mmShyGCa4nPhfGCLQ.jpg",
            "voteAverage": 6.563,
            "voteCount": 79,
            "id": 22314,
            "mediaType": 1,
            "popularity": 8.91
          },
          {
            "adult": false,
            "originalTitle": "Youth in Revolt",
            "releaseDate": "2009-09-11T00:00:00",
            "title": "Youth in Revolt",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/xc5gJeL5Mvt4cERgPm43Q7Th7yI.jpg",
            "genreIds": [
              35,
              10749
            ],
            "originalLanguage": "en",
            "overview": "As a fan of Albert Camus and Jean-Luc Godard, teenage Nick Twisp is definitely out of his element when his mother and her boyfriend move the family to a trailer park. When a pretty neighbor named Sheeni plays records by French crooners, it's love at first sight for frustrated and inexperienced Nick. Learning that she is dating someone, Nick launches a hilarious quest to find his way into Sheeni's heart -- and bed.",
            "posterPath": "https://image.tmdb.org/t/p/original/3J48x7Utjx0sdYIvjsMWnlEbxpV.jpg",
            "voteAverage": 6.173,
            "voteCount": 799,
            "id": 22327,
            "mediaType": 1,
            "popularity": 9.261
          },
          {
            "adult": false,
            "originalTitle": "Reach for the Sky",
            "releaseDate": "1956-07-05T00:00:00",
            "title": "Reach for the Sky",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/pujpNHRw5wFJNmRy8upv75WWywn.jpg",
            "genreIds": [
              10752,
              36,
              18
            ],
            "originalLanguage": "en",
            "overview": "The true story of airman Douglas Bader who overcame the loss of both legs in a 1931 flying accident to become a successful fighter pilot and wing leader during World War II.",
            "posterPath": "https://image.tmdb.org/t/p/original/2OKZUqjoIC71vkJ8SUR7DzvXAvw.jpg",
            "voteAverage": 6.707,
            "voteCount": 29,
            "id": 22350,
            "mediaType": 1,
            "popularity": 2.916
          },
          {
            "adult": false,
            "originalTitle": "Бег иноходца",
            "releaseDate": "1969-12-01T00:00:00",
            "title": "Goodbye, Gyulsary!",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/2zk7lJmc2cDTPKNJkpI1ZzA1rwZ.jpg",
            "genreIds": [
              18
            ],
            "originalLanguage": "ru",
            "overview": "Based on Chingiz Aitmatov’s novel \"Farewell, Gyulsary!\" The story of Tanabai the blacksmith, father of three children, who upon his return from war became a herdsman, and his tragic love for the soldier’s widow Byubyudzhan. The lyrical poem has an additional storyline concerning the horse Gyulsary and his master Tanabai.",
            "posterPath": "https://image.tmdb.org/t/p/original/wZu0azU5tOVS4GDHZvjL6b8pajc.jpg",
            "voteAverage": 6.4,
            "voteCount": 7,
            "id": 272643,
            "mediaType": 1,
            "popularity": 0.6
          },
          {
            "adult": false,
            "originalTitle": "Пой песню, поэт…",
            "releaseDate": "1973-03-22T00:00:00",
            "title": "Sing a Song, Poet",
            "video": false,
            "backdropPath": "https://image.tmdb.org/t/p/original/oB1edPL7DSBO4gk89dISRevk7ix.jpg",
            "genreIds": [
              18
            ],
            "originalLanguage": "ru",
            "overview": "About the life and work of the poet Sergei Yesenin, his connection with his native country, its people and nature. Childhood, love, painful searches for his place in the new, revolutionary Russia — everything found a place in Yesenin's lyrics. Frames illustrating Yesenin's poetry and poems are side by side in the film with episodes of the poet's biography: the film reflects the days of his stay in America, World War I, revolution and village round dances, a daring uncle, a wise mother...",
            "posterPath": "https://image.tmdb.org/t/p/original/wNVZeIFDKkEzp2Wmzk5co24RJ8D.jpg",
            "voteAverage": 3.7,
            "voteCount": 6,
            "id": 272647,
            "mediaType": 1,
            "popularity": 0.762
          }
        ],
        "totalPages": 616,
        "totalResults": 12311
      },
      "recommendations": {
        "page": 1,
        "results": [],
        "totalPages": 0,
        "totalResults": 0
      },
      "spokenLanguages": [
        {
          "iso_639_1": "de",
          "name": "Deutsch"
        },
        {
          "iso_639_1": "en",
          "name": "English"
        }
      ],
      "status": "Post Production",
      "tagline": "The world forever changes.",
      "title": "Oppenheimer",
      "video": true,
      "videos": {
        "id": 872585,
        "results": [
          {
            "id": "6458a73277d23b0170372259",
            "iso_3166_1": "US",
            "iso_639_1": "en",
            "key": "uYPbbksJxIg",
            "name": "New Trailer",
            "site": "YouTube",
            "size": 1080,
            "type": "Trailer"
          },
          {
            "id": "639fb365223e20007d3d3618",
            "iso_3166_1": "US",
            "iso_639_1": "en",
            "key": "bK6ldnjE3Y0",
            "name": "Official Trailer",
            "site": "YouTube",
            "size": 1080,
            "type": "Trailer"
          },
          {
            "id": "62e27b8f48138212c88cf337",
            "iso_3166_1": "US",
            "iso_639_1": "en",
            "key": "hflCiNtY6MA",
            "name": "Oppenheimer Announcement",
            "site": "YouTube",
            "size": 1080,
            "type": "Teaser"
          }
        ]
      },
      "voteAverage": 0,
      "voteCount": 0
    }
  }

  savePlayer(player: YT.Player) {
    this.player = player;
  }

  onStateChange(event: any) {
    console.log('player state', event.data);
  }

  getMovieData() {
    this.loading = true;
    this.movieService.getMovieDetails(this.movieId)
      .subscribe((response: MovieDetails) => {
          console.log(response);
          this.movie = response
        },
        (error) => {
          this.alertService.error(error);
        });
    this.loading = false;
  }

  handleImageError(event: any) {
    event.target.src = "./assets/img/placeholder.jpg"
  }
}
