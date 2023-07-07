import {Component, OnInit} from '@angular/core';
import {Movie} from '../../../core/models/movie';
import {MovieService} from '../../../core/services/movie.service';
import {AlertService} from '../../../shared/services/alert.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})

export class MoviesComponent implements OnInit {
  isLoading: boolean = false;
  nowPlayingMovie: Movie[] = [];
  popularMovies: Movie[] = [
    {
      "adult": false,
      "originalTitle": "The Godfather",
      "releaseDate": "1972-03-14T00:00:00",
      "title": "The Godfather",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/tmU7GeKVybMWFButWEGl2M4GeiP.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.",
      "posterPath": "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg",
      "voteAverage": 8.7,
      "voteCount": 18176,
      "id": 238,
      "mediaType": 1,
      "popularity": 108.62
    },
    {
      "adult": false,
      "originalTitle": "The Shawshank Redemption",
      "releaseDate": "1994-09-23T00:00:00",
      "title": "The Shawshank Redemption",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/kXfqcdQKsToO0OUXHcrrNCHDBzO.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "Framed in the 1940s for the double murder of his wife and her lover, upstanding banker Andy Dufresne begins a new life at the Shawshank prison, where he puts his accounting skills to work for an amoral warden. During his long stretch in prison, Dufresne comes to be admired by the other inmates -- including an older prisoner named Red -- for his integrity and unquenchable sense of hope.",
      "posterPath": "https://image.tmdb.org/t/p/original/lyQBXzOQSuE59IsHyhrp0qIiPAz.jpg",
      "voteAverage": 8.7,
      "voteCount": 24062,
      "id": 278,
      "mediaType": 1,
      "popularity": 83.323
    },
    {
      "adult": false,
      "originalTitle": "The Godfather Part II",
      "releaseDate": "1974-12-20T00:00:00",
      "title": "The Godfather Part II",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/kGzFbGhp99zva6oZODW5atUtnqi.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "In the continuing saga of the Corleone crime family, a young Vito Corleone grows up in Sicily and in 1910s New York. In the 1950s, Michael Corleone attempts to expand the family business into Las Vegas, Hollywood and Cuba.",
      "posterPath": "https://image.tmdb.org/t/p/original/bMadFzhjy9T7R8J48QGq1ngWNAK.jpg",
      "voteAverage": 8.6,
      "voteCount": 10966,
      "id": 240,
      "mediaType": 1,
      "popularity": 50.449
    },
    {
      "adult": false,
      "originalTitle": "दिलवाले दुल्हनिया ले जायेंगे",
      "releaseDate": "1995-10-20T00:00:00",
      "title": "Dilwale Dulhania Le Jayenge",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/vI3aUGTuRRdM7J78KIdW98LdxE5.jpg",
      "genreIds": [
        35,
        18,
        10749
      ],
      "originalLanguage": "hi",
      "overview": "Raj is a rich, carefree, happy-go-lucky second generation NRI. Simran is the daughter of Chaudhary Baldev Singh, who in spite of being an NRI is very strict about adherence to Indian values. Simran has left for India to be married to her childhood fiancé. Raj leaves for India with a mission at his hands, to claim his lady love under the noses of her whole family. Thus begins a saga.",
      "posterPath": "https://image.tmdb.org/t/p/original/ktejodbcdCPXbMMdnpI9BUxW6O8.jpg",
      "voteAverage": 8.6,
      "voteCount": 4162,
      "id": 19404,
      "mediaType": 1,
      "popularity": 23.182
    },
    {
      "adult": false,
      "originalTitle": "Schindler's List",
      "releaseDate": "1993-12-15T00:00:00",
      "title": "Schindler's List",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/zb6fM1CX41D9rF9hdgclu0peUmy.jpg",
      "genreIds": [
        18,
        36,
        10752
      ],
      "originalLanguage": "en",
      "overview": "The true story of how businessman Oskar Schindler saved over a thousand Jewish lives from the Nazis while they worked as slaves in his factory during World War II.",
      "posterPath": "https://image.tmdb.org/t/p/original/sF1U4EUQS8YHUYjNl3pMGNIQyr0.jpg",
      "voteAverage": 8.6,
      "voteCount": 14223,
      "id": 424,
      "mediaType": 1,
      "popularity": 42.91
    },
    {
      "adult": false,
      "originalTitle": "12 Angry Men",
      "releaseDate": "1957-04-10T00:00:00",
      "title": "12 Angry Men",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qqHQsStV6exghCM7zbObuYBiYxw.jpg",
      "genreIds": [
        18
      ],
      "originalLanguage": "en",
      "overview": "The defense and the prosecution have rested and the jury is filing into the jury room to decide if a young Spanish-American is guilty or innocent of murdering his father. What begins as an open and shut case soon becomes a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other.",
      "posterPath": "https://image.tmdb.org/t/p/original/ow3wq89wM8qd5X7hWKxiRfsFf9C.jpg",
      "voteAverage": 8.5,
      "voteCount": 7377,
      "id": 389,
      "mediaType": 1,
      "popularity": 38.807
    },
    {
      "adult": false,
      "originalTitle": "千と千尋の神隠し",
      "releaseDate": "2001-07-20T00:00:00",
      "title": "Spirited Away",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/Ab8mkHmkYADjU7wQiOkia9BzGvS.jpg",
      "genreIds": [
        16,
        10751,
        14
      ],
      "originalLanguage": "ja",
      "overview": "A young girl, Chihiro, becomes trapped in a strange new world of spirits. When her parents undergo a mysterious transformation, she must call upon the courage she never knew she had to free her family.",
      "posterPath": "https://image.tmdb.org/t/p/original/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg",
      "voteAverage": 8.5,
      "voteCount": 14501,
      "id": 129,
      "mediaType": 1,
      "popularity": 72.421
    },
    {
      "adult": false,
      "originalTitle": "Spider-Man: Across the Spider-Verse",
      "releaseDate": "2023-05-31T00:00:00",
      "title": "Spider-Man: Across the Spider-Verse",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/4HodYYKEIsGOdinkGi2Ucz6X9i0.jpg",
      "genreIds": [
        28,
        12,
        16,
        878
      ],
      "originalLanguage": "en",
      "overview": "After reuniting with Gwen Stacy, Brooklyn’s full-time, friendly neighborhood Spider-Man is catapulted across the Multiverse, where he encounters the Spider Society, a team of Spider-People charged with protecting the Multiverse’s very existence. But when the heroes clash on how to handle a new threat, Miles finds himself pitted against the other Spiders and must set out on his own to save those he loves most.",
      "posterPath": "https://image.tmdb.org/t/p/original/8Vt6mWEReuy4Of61Lnj5Xj704m8.jpg",
      "voteAverage": 8.5,
      "voteCount": 2052,
      "id": 569094,
      "mediaType": 1,
      "popularity": 1493.297
    },
    {
      "adult": false,
      "originalTitle": "君の名は。",
      "releaseDate": "2016-08-26T00:00:00",
      "title": "Your Name.",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/dIWwZW7dJJtqC6CgWzYkNVKIUm8.jpg",
      "genreIds": [
        10749,
        16,
        18
      ],
      "originalLanguage": "ja",
      "overview": "High schoolers Mitsuha and Taki are complete strangers living separate lives. But one night, they suddenly switch places. Mitsuha wakes up in Taki’s body, and he in hers. This bizarre occurrence continues to happen randomly, and the two must adjust their lives around each other.",
      "posterPath": "https://image.tmdb.org/t/p/original/q719jXXEzOoYaps6babgKnONONX.jpg",
      "voteAverage": 8.5,
      "voteCount": 9988,
      "id": 372058,
      "mediaType": 1,
      "popularity": 75.256
    },
    {
      "adult": false,
      "originalTitle": "기생충",
      "releaseDate": "2019-05-30T00:00:00",
      "title": "Parasite",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/hiKmpZMGZsrkA3cdce8a7Dpos1j.jpg",
      "genreIds": [
        35,
        53,
        18
      ],
      "originalLanguage": "ko",
      "overview": "All unemployed, Ki-taek's family takes peculiar interest in the wealthy and glamorous Parks for their livelihood until they get entangled in an unexpected incident.",
      "posterPath": "https://image.tmdb.org/t/p/original/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg",
      "voteAverage": 8.5,
      "voteCount": 15945,
      "id": 496243,
      "mediaType": 1,
      "popularity": 65.796
    },
    {
      "adult": false,
      "originalTitle": "The Green Mile",
      "releaseDate": "1999-12-10T00:00:00",
      "title": "The Green Mile",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/l6hQWH9eDksNJNiXWYRkWqikOdu.jpg",
      "genreIds": [
        14,
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "A supernatural tale set on death row in a Southern prison, where gentle giant John Coffey possesses the mysterious power to heal people's ailments. When the cell block's head guard, Paul Edgecomb, recognizes Coffey's miraculous gift, he tries desperately to help stave off the condemned man's execution.",
      "posterPath": "https://image.tmdb.org/t/p/original/o0lO84GI7qrG6XFvtsPOSV7CTNa.jpg",
      "voteAverage": 8.5,
      "voteCount": 15564,
      "id": 497,
      "mediaType": 1,
      "popularity": 56.178
    },
    {
      "adult": false,
      "originalTitle": "The Dark Knight",
      "releaseDate": "2008-07-14T00:00:00",
      "title": "The Dark Knight",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/dqK9Hag1054tghRQSqLSfrkvQnA.jpg",
      "genreIds": [
        18,
        28,
        80,
        53
      ],
      "originalLanguage": "en",
      "overview": "Batman raises the stakes in his war on crime. With the help of Lt. Jim Gordon and District Attorney Harvey Dent, Batman sets out to dismantle the remaining criminal organizations that plague the streets. The partnership proves to be effective, but they soon find themselves prey to a reign of chaos unleashed by a rising criminal mastermind known to the terrified citizens of Gotham as the Joker.",
      "posterPath": "https://image.tmdb.org/t/p/original/qJ2tW6WMUDux911r6m7haRef0WH.jpg",
      "voteAverage": 8.5,
      "voteCount": 29991,
      "id": 155,
      "mediaType": 1,
      "popularity": 79.414
    },
    {
      "adult": false,
      "originalTitle": "Cuando Sea Joven",
      "releaseDate": "2022-09-14T00:00:00",
      "title": "Cuando Sea Joven",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/ejnlCzBd5pOGAYCpxC93NXSrdrz.jpg",
      "genreIds": [
        35,
        14
      ],
      "originalLanguage": "es",
      "overview": "70-year-old Malena gets a second chance at life when she magically turns into her 22-year-old self. Now, posing as \"Maria\" to hide her true identity, she becomes the lead singer of her grandson's band and tries to recover her dream of singing, which she had to give up at some point.",
      "posterPath": "https://image.tmdb.org/t/p/original/6gIJuFHh5Lj4dNaPG3TzIMl7L68.jpg",
      "voteAverage": 8.5,
      "voteCount": 243,
      "id": 772071,
      "mediaType": 1,
      "popularity": 13.533
    },
    {
      "adult": false,
      "originalTitle": "Pulp Fiction",
      "releaseDate": "1994-09-10T00:00:00",
      "title": "Pulp Fiction",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/suaEOtk1N1sgg2MTM7oZd2cfVp3.jpg",
      "genreIds": [
        53,
        80
      ],
      "originalLanguage": "en",
      "overview": "A burger-loving hit man, his philosophical partner, a drug-addled gangster's moll and a washed-up boxer converge in this sprawling, comedic crime caper. Their adventures unfurl in three stories that ingeniously trip back and forth in time.",
      "posterPath": "https://image.tmdb.org/t/p/original/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg",
      "voteAverage": 8.5,
      "voteCount": 25383,
      "id": 680,
      "mediaType": 1,
      "popularity": 68.994
    },
    {
      "adult": false,
      "originalTitle": "Il buono, il brutto, il cattivo",
      "releaseDate": "1966-12-23T00:00:00",
      "title": "The Good, the Bad and the Ugly",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/eoCSp75lxatmIa6aGqfnzwtbttd.jpg",
      "genreIds": [
        37
      ],
      "originalLanguage": "it",
      "overview": "While the Civil War rages on between the Union and the Confederacy, three men – a quiet loner, a ruthless hitman, and a Mexican bandit – comb the American Southwest in search of a strongbox containing $200,000 in stolen gold.",
      "posterPath": "https://image.tmdb.org/t/p/original/bX2xnavhMYjWDoZp1VM6VnU1xwe.jpg",
      "voteAverage": 8.5,
      "voteCount": 7495,
      "id": 429,
      "mediaType": 1,
      "popularity": 51.484
    },
    {
      "adult": false,
      "originalTitle": "Forrest Gump",
      "releaseDate": "1994-06-23T00:00:00",
      "title": "Forrest Gump",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qdIMHd4sEfJSckfVJfKQvisL02a.jpg",
      "genreIds": [
        35,
        18,
        10749
      ],
      "originalLanguage": "en",
      "overview": "A man with a low IQ has accomplished great things in his life and been present during significant historic events—in each case, far exceeding what anyone imagined he could do. But despite all he has achieved, his one true love eludes him.",
      "posterPath": "https://image.tmdb.org/t/p/original/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg",
      "voteAverage": 8.5,
      "voteCount": 24899,
      "id": 13,
      "mediaType": 1,
      "popularity": 63.661
    },
    {
      "adult": false,
      "originalTitle": "The Lord of the Rings: The Return of the King",
      "releaseDate": "2003-12-01T00:00:00",
      "title": "The Lord of the Rings: The Return of the King",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/2u7zbn8EudG6kLlBzUYqP8RyFU4.jpg",
      "genreIds": [
        12,
        14,
        28
      ],
      "originalLanguage": "en",
      "overview": "Aragorn is revealed as the heir to the ancient kings as he, Gandalf and the other members of the broken fellowship struggle to save Gondor from Sauron's forces. Meanwhile, Frodo and Sam take the ring closer to the heart of Mordor, the dark lord's realm.",
      "posterPath": "https://image.tmdb.org/t/p/original/rCzpDGLbOoPwLjy3OAm5NUPOTrC.jpg",
      "voteAverage": 8.5,
      "voteCount": 21796,
      "id": 122,
      "mediaType": 1,
      "popularity": 69.535
    },
    {
      "adult": false,
      "originalTitle": "Primal: Tales of Savagery",
      "releaseDate": "2019-11-21T00:00:00",
      "title": "Primal: Tales of Savagery",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/uBZQOYZLIU9dBmd62fdzBAoropu.jpg",
      "genreIds": [
        28,
        12,
        16,
        18
      ],
      "originalLanguage": "en",
      "overview": "Genndy Tartakovsky's Primal: Tales of Savagery features a caveman and a dinosaur on the brink of extinction. Bonded by tragedy, this unlikely friendship becomes the only hope of survival.",
      "posterPath": "https://image.tmdb.org/t/p/original/9NBBkdxH0TjQEBSN2AzeE1sgsF9.jpg",
      "voteAverage": 8.5,
      "voteCount": 268,
      "id": 704264,
      "mediaType": 1,
      "popularity": 14.24
    },
    {
      "adult": false,
      "originalTitle": "GoodFellas",
      "releaseDate": "1990-09-12T00:00:00",
      "title": "GoodFellas",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/sw7mordbZxgITU877yTpZCud90M.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "The true story of Henry Hill, a half-Irish, half-Sicilian Brooklyn kid who is adopted by neighbourhood gangsters at an early age and climbs the ranks of a Mafia family under the guidance of Jimmy Conway.",
      "posterPath": "https://image.tmdb.org/t/p/original/aKuFiU82s5ISJpGZp7YkIr3kCUd.jpg",
      "voteAverage": 8.5,
      "voteCount": 11388,
      "id": 769,
      "mediaType": 1,
      "popularity": 38.29
    },
    {
      "adult": false,
      "originalTitle": "七人の侍",
      "releaseDate": "1954-04-26T00:00:00",
      "title": "Seven Samurai",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qvZ91FwMq6O47VViAr8vZNQz3WI.jpg",
      "genreIds": [
        28,
        18
      ],
      "originalLanguage": "ja",
      "overview": "A samurai answers a village's request for protection after he falls on hard times. The town needs protection from bandits, so the samurai gathers six others to help him teach the people how to defend themselves, and the villagers provide the soldiers with food.",
      "posterPath": "https://image.tmdb.org/t/p/original/8OKmBV5BUFzmozIC3pPWKHy17kx.jpg",
      "voteAverage": 8.5,
      "voteCount": 3100,
      "id": 346,
      "mediaType": 1,
      "popularity": 30.408
    }
  ];
  topRatedMovies: Movie[] = [
    {
      "adult": false,
      "originalTitle": "The Godfather",
      "releaseDate": "1972-03-14T00:00:00",
      "title": "The Godfather",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/tmU7GeKVybMWFButWEGl2M4GeiP.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.",
      "posterPath": "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg",
      "voteAverage": 8.7,
      "voteCount": 18176,
      "id": 238,
      "mediaType": 1,
      "popularity": 108.62
    },
    {
      "adult": false,
      "originalTitle": "The Shawshank Redemption",
      "releaseDate": "1994-09-23T00:00:00",
      "title": "The Shawshank Redemption",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/kXfqcdQKsToO0OUXHcrrNCHDBzO.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "Framed in the 1940s for the double murder of his wife and her lover, upstanding banker Andy Dufresne begins a new life at the Shawshank prison, where he puts his accounting skills to work for an amoral warden. During his long stretch in prison, Dufresne comes to be admired by the other inmates -- including an older prisoner named Red -- for his integrity and unquenchable sense of hope.",
      "posterPath": "https://image.tmdb.org/t/p/original/lyQBXzOQSuE59IsHyhrp0qIiPAz.jpg",
      "voteAverage": 8.7,
      "voteCount": 24062,
      "id": 278,
      "mediaType": 1,
      "popularity": 83.323
    },
    {
      "adult": false,
      "originalTitle": "The Godfather Part II",
      "releaseDate": "1974-12-20T00:00:00",
      "title": "The Godfather Part II",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/kGzFbGhp99zva6oZODW5atUtnqi.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "In the continuing saga of the Corleone crime family, a young Vito Corleone grows up in Sicily and in 1910s New York. In the 1950s, Michael Corleone attempts to expand the family business into Las Vegas, Hollywood and Cuba.",
      "posterPath": "https://image.tmdb.org/t/p/original/bMadFzhjy9T7R8J48QGq1ngWNAK.jpg",
      "voteAverage": 8.6,
      "voteCount": 10966,
      "id": 240,
      "mediaType": 1,
      "popularity": 50.449
    },
    {
      "adult": false,
      "originalTitle": "दिलवाले दुल्हनिया ले जायेंगे",
      "releaseDate": "1995-10-20T00:00:00",
      "title": "Dilwale Dulhania Le Jayenge",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/vI3aUGTuRRdM7J78KIdW98LdxE5.jpg",
      "genreIds": [
        35,
        18,
        10749
      ],
      "originalLanguage": "hi",
      "overview": "Raj is a rich, carefree, happy-go-lucky second generation NRI. Simran is the daughter of Chaudhary Baldev Singh, who in spite of being an NRI is very strict about adherence to Indian values. Simran has left for India to be married to her childhood fiancé. Raj leaves for India with a mission at his hands, to claim his lady love under the noses of her whole family. Thus begins a saga.",
      "posterPath": "https://image.tmdb.org/t/p/original/ktejodbcdCPXbMMdnpI9BUxW6O8.jpg",
      "voteAverage": 8.6,
      "voteCount": 4162,
      "id": 19404,
      "mediaType": 1,
      "popularity": 23.182
    },
    {
      "adult": false,
      "originalTitle": "Schindler's List",
      "releaseDate": "1993-12-15T00:00:00",
      "title": "Schindler's List",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/zb6fM1CX41D9rF9hdgclu0peUmy.jpg",
      "genreIds": [
        18,
        36,
        10752
      ],
      "originalLanguage": "en",
      "overview": "The true story of how businessman Oskar Schindler saved over a thousand Jewish lives from the Nazis while they worked as slaves in his factory during World War II.",
      "posterPath": "https://image.tmdb.org/t/p/original/sF1U4EUQS8YHUYjNl3pMGNIQyr0.jpg",
      "voteAverage": 8.6,
      "voteCount": 14223,
      "id": 424,
      "mediaType": 1,
      "popularity": 42.91
    },
    {
      "adult": false,
      "originalTitle": "12 Angry Men",
      "releaseDate": "1957-04-10T00:00:00",
      "title": "12 Angry Men",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qqHQsStV6exghCM7zbObuYBiYxw.jpg",
      "genreIds": [
        18
      ],
      "originalLanguage": "en",
      "overview": "The defense and the prosecution have rested and the jury is filing into the jury room to decide if a young Spanish-American is guilty or innocent of murdering his father. What begins as an open and shut case soon becomes a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other.",
      "posterPath": "https://image.tmdb.org/t/p/original/ow3wq89wM8qd5X7hWKxiRfsFf9C.jpg",
      "voteAverage": 8.5,
      "voteCount": 7377,
      "id": 389,
      "mediaType": 1,
      "popularity": 38.807
    },
    {
      "adult": false,
      "originalTitle": "千と千尋の神隠し",
      "releaseDate": "2001-07-20T00:00:00",
      "title": "Spirited Away",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/Ab8mkHmkYADjU7wQiOkia9BzGvS.jpg",
      "genreIds": [
        16,
        10751,
        14
      ],
      "originalLanguage": "ja",
      "overview": "A young girl, Chihiro, becomes trapped in a strange new world of spirits. When her parents undergo a mysterious transformation, she must call upon the courage she never knew she had to free her family.",
      "posterPath": "https://image.tmdb.org/t/p/original/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg",
      "voteAverage": 8.5,
      "voteCount": 14501,
      "id": 129,
      "mediaType": 1,
      "popularity": 72.421
    },
    {
      "adult": false,
      "originalTitle": "Spider-Man: Across the Spider-Verse",
      "releaseDate": "2023-05-31T00:00:00",
      "title": "Spider-Man: Across the Spider-Verse",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/4HodYYKEIsGOdinkGi2Ucz6X9i0.jpg",
      "genreIds": [
        28,
        12,
        16,
        878
      ],
      "originalLanguage": "en",
      "overview": "After reuniting with Gwen Stacy, Brooklyn’s full-time, friendly neighborhood Spider-Man is catapulted across the Multiverse, where he encounters the Spider Society, a team of Spider-People charged with protecting the Multiverse’s very existence. But when the heroes clash on how to handle a new threat, Miles finds himself pitted against the other Spiders and must set out on his own to save those he loves most.",
      "posterPath": "https://image.tmdb.org/t/p/original/8Vt6mWEReuy4Of61Lnj5Xj704m8.jpg",
      "voteAverage": 8.5,
      "voteCount": 2052,
      "id": 569094,
      "mediaType": 1,
      "popularity": 1493.297
    },
    {
      "adult": false,
      "originalTitle": "君の名は。",
      "releaseDate": "2016-08-26T00:00:00",
      "title": "Your Name.",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/dIWwZW7dJJtqC6CgWzYkNVKIUm8.jpg",
      "genreIds": [
        10749,
        16,
        18
      ],
      "originalLanguage": "ja",
      "overview": "High schoolers Mitsuha and Taki are complete strangers living separate lives. But one night, they suddenly switch places. Mitsuha wakes up in Taki’s body, and he in hers. This bizarre occurrence continues to happen randomly, and the two must adjust their lives around each other.",
      "posterPath": "https://image.tmdb.org/t/p/original/q719jXXEzOoYaps6babgKnONONX.jpg",
      "voteAverage": 8.5,
      "voteCount": 9988,
      "id": 372058,
      "mediaType": 1,
      "popularity": 75.256
    },
    {
      "adult": false,
      "originalTitle": "기생충",
      "releaseDate": "2019-05-30T00:00:00",
      "title": "Parasite",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/hiKmpZMGZsrkA3cdce8a7Dpos1j.jpg",
      "genreIds": [
        35,
        53,
        18
      ],
      "originalLanguage": "ko",
      "overview": "All unemployed, Ki-taek's family takes peculiar interest in the wealthy and glamorous Parks for their livelihood until they get entangled in an unexpected incident.",
      "posterPath": "https://image.tmdb.org/t/p/original/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg",
      "voteAverage": 8.5,
      "voteCount": 15945,
      "id": 496243,
      "mediaType": 1,
      "popularity": 65.796
    },
    {
      "adult": false,
      "originalTitle": "The Green Mile",
      "releaseDate": "1999-12-10T00:00:00",
      "title": "The Green Mile",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/l6hQWH9eDksNJNiXWYRkWqikOdu.jpg",
      "genreIds": [
        14,
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "A supernatural tale set on death row in a Southern prison, where gentle giant John Coffey possesses the mysterious power to heal people's ailments. When the cell block's head guard, Paul Edgecomb, recognizes Coffey's miraculous gift, he tries desperately to help stave off the condemned man's execution.",
      "posterPath": "https://image.tmdb.org/t/p/original/o0lO84GI7qrG6XFvtsPOSV7CTNa.jpg",
      "voteAverage": 8.5,
      "voteCount": 15564,
      "id": 497,
      "mediaType": 1,
      "popularity": 56.178
    },
    {
      "adult": false,
      "originalTitle": "The Dark Knight",
      "releaseDate": "2008-07-14T00:00:00",
      "title": "The Dark Knight",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/dqK9Hag1054tghRQSqLSfrkvQnA.jpg",
      "genreIds": [
        18,
        28,
        80,
        53
      ],
      "originalLanguage": "en",
      "overview": "Batman raises the stakes in his war on crime. With the help of Lt. Jim Gordon and District Attorney Harvey Dent, Batman sets out to dismantle the remaining criminal organizations that plague the streets. The partnership proves to be effective, but they soon find themselves prey to a reign of chaos unleashed by a rising criminal mastermind known to the terrified citizens of Gotham as the Joker.",
      "posterPath": "https://image.tmdb.org/t/p/original/qJ2tW6WMUDux911r6m7haRef0WH.jpg",
      "voteAverage": 8.5,
      "voteCount": 29991,
      "id": 155,
      "mediaType": 1,
      "popularity": 79.414
    },
    {
      "adult": false,
      "originalTitle": "Cuando Sea Joven",
      "releaseDate": "2022-09-14T00:00:00",
      "title": "Cuando Sea Joven",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/ejnlCzBd5pOGAYCpxC93NXSrdrz.jpg",
      "genreIds": [
        35,
        14
      ],
      "originalLanguage": "es",
      "overview": "70-year-old Malena gets a second chance at life when she magically turns into her 22-year-old self. Now, posing as \"Maria\" to hide her true identity, she becomes the lead singer of her grandson's band and tries to recover her dream of singing, which she had to give up at some point.",
      "posterPath": "https://image.tmdb.org/t/p/original/6gIJuFHh5Lj4dNaPG3TzIMl7L68.jpg",
      "voteAverage": 8.5,
      "voteCount": 243,
      "id": 772071,
      "mediaType": 1,
      "popularity": 13.533
    },
    {
      "adult": false,
      "originalTitle": "Pulp Fiction",
      "releaseDate": "1994-09-10T00:00:00",
      "title": "Pulp Fiction",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/suaEOtk1N1sgg2MTM7oZd2cfVp3.jpg",
      "genreIds": [
        53,
        80
      ],
      "originalLanguage": "en",
      "overview": "A burger-loving hit man, his philosophical partner, a drug-addled gangster's moll and a washed-up boxer converge in this sprawling, comedic crime caper. Their adventures unfurl in three stories that ingeniously trip back and forth in time.",
      "posterPath": "https://image.tmdb.org/t/p/original/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg",
      "voteAverage": 8.5,
      "voteCount": 25383,
      "id": 680,
      "mediaType": 1,
      "popularity": 68.994
    },
    {
      "adult": false,
      "originalTitle": "Il buono, il brutto, il cattivo",
      "releaseDate": "1966-12-23T00:00:00",
      "title": "The Good, the Bad and the Ugly",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/eoCSp75lxatmIa6aGqfnzwtbttd.jpg",
      "genreIds": [
        37
      ],
      "originalLanguage": "it",
      "overview": "While the Civil War rages on between the Union and the Confederacy, three men – a quiet loner, a ruthless hitman, and a Mexican bandit – comb the American Southwest in search of a strongbox containing $200,000 in stolen gold.",
      "posterPath": "https://image.tmdb.org/t/p/original/bX2xnavhMYjWDoZp1VM6VnU1xwe.jpg",
      "voteAverage": 8.5,
      "voteCount": 7495,
      "id": 429,
      "mediaType": 1,
      "popularity": 51.484
    },
    {
      "adult": false,
      "originalTitle": "Forrest Gump",
      "releaseDate": "1994-06-23T00:00:00",
      "title": "Forrest Gump",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qdIMHd4sEfJSckfVJfKQvisL02a.jpg",
      "genreIds": [
        35,
        18,
        10749
      ],
      "originalLanguage": "en",
      "overview": "A man with a low IQ has accomplished great things in his life and been present during significant historic events—in each case, far exceeding what anyone imagined he could do. But despite all he has achieved, his one true love eludes him.",
      "posterPath": "https://image.tmdb.org/t/p/original/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg",
      "voteAverage": 8.5,
      "voteCount": 24899,
      "id": 13,
      "mediaType": 1,
      "popularity": 63.661
    },
    {
      "adult": false,
      "originalTitle": "The Lord of the Rings: The Return of the King",
      "releaseDate": "2003-12-01T00:00:00",
      "title": "The Lord of the Rings: The Return of the King",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/2u7zbn8EudG6kLlBzUYqP8RyFU4.jpg",
      "genreIds": [
        12,
        14,
        28
      ],
      "originalLanguage": "en",
      "overview": "Aragorn is revealed as the heir to the ancient kings as he, Gandalf and the other members of the broken fellowship struggle to save Gondor from Sauron's forces. Meanwhile, Frodo and Sam take the ring closer to the heart of Mordor, the dark lord's realm.",
      "posterPath": "https://image.tmdb.org/t/p/original/rCzpDGLbOoPwLjy3OAm5NUPOTrC.jpg",
      "voteAverage": 8.5,
      "voteCount": 21796,
      "id": 122,
      "mediaType": 1,
      "popularity": 69.535
    },
    {
      "adult": false,
      "originalTitle": "Primal: Tales of Savagery",
      "releaseDate": "2019-11-21T00:00:00",
      "title": "Primal: Tales of Savagery",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/uBZQOYZLIU9dBmd62fdzBAoropu.jpg",
      "genreIds": [
        28,
        12,
        16,
        18
      ],
      "originalLanguage": "en",
      "overview": "Genndy Tartakovsky's Primal: Tales of Savagery features a caveman and a dinosaur on the brink of extinction. Bonded by tragedy, this unlikely friendship becomes the only hope of survival.",
      "posterPath": "https://image.tmdb.org/t/p/original/9NBBkdxH0TjQEBSN2AzeE1sgsF9.jpg",
      "voteAverage": 8.5,
      "voteCount": 268,
      "id": 704264,
      "mediaType": 1,
      "popularity": 14.24
    },
    {
      "adult": false,
      "originalTitle": "GoodFellas",
      "releaseDate": "1990-09-12T00:00:00",
      "title": "GoodFellas",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/sw7mordbZxgITU877yTpZCud90M.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "The true story of Henry Hill, a half-Irish, half-Sicilian Brooklyn kid who is adopted by neighbourhood gangsters at an early age and climbs the ranks of a Mafia family under the guidance of Jimmy Conway.",
      "posterPath": "https://image.tmdb.org/t/p/original/aKuFiU82s5ISJpGZp7YkIr3kCUd.jpg",
      "voteAverage": 8.5,
      "voteCount": 11388,
      "id": 769,
      "mediaType": 1,
      "popularity": 38.29
    },
    {
      "adult": false,
      "originalTitle": "七人の侍",
      "releaseDate": "1954-04-26T00:00:00",
      "title": "Seven Samurai",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qvZ91FwMq6O47VViAr8vZNQz3WI.jpg",
      "genreIds": [
        28,
        18
      ],
      "originalLanguage": "ja",
      "overview": "A samurai answers a village's request for protection after he falls on hard times. The town needs protection from bandits, so the samurai gathers six others to help him teach the people how to defend themselves, and the villagers provide the soldiers with food.",
      "posterPath": "https://image.tmdb.org/t/p/original/8OKmBV5BUFzmozIC3pPWKHy17kx.jpg",
      "voteAverage": 8.5,
      "voteCount": 3100,
      "id": 346,
      "mediaType": 1,
      "popularity": 30.408
    }
  ];
  upcomingMovies: Movie[] = [
    {
      "adult": false,
      "originalTitle": "The Godfather",
      "releaseDate": "1972-03-14T00:00:00",
      "title": "The Godfather",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/tmU7GeKVybMWFButWEGl2M4GeiP.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.",
      "posterPath": "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg",
      "voteAverage": 8.7,
      "voteCount": 18176,
      "id": 238,
      "mediaType": 1,
      "popularity": 108.62
    },
    {
      "adult": false,
      "originalTitle": "The Shawshank Redemption",
      "releaseDate": "1994-09-23T00:00:00",
      "title": "The Shawshank Redemption",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/kXfqcdQKsToO0OUXHcrrNCHDBzO.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "Framed in the 1940s for the double murder of his wife and her lover, upstanding banker Andy Dufresne begins a new life at the Shawshank prison, where he puts his accounting skills to work for an amoral warden. During his long stretch in prison, Dufresne comes to be admired by the other inmates -- including an older prisoner named Red -- for his integrity and unquenchable sense of hope.",
      "posterPath": "https://image.tmdb.org/t/p/original/lyQBXzOQSuE59IsHyhrp0qIiPAz.jpg",
      "voteAverage": 8.7,
      "voteCount": 24062,
      "id": 278,
      "mediaType": 1,
      "popularity": 83.323
    },
    {
      "adult": false,
      "originalTitle": "The Godfather Part II",
      "releaseDate": "1974-12-20T00:00:00",
      "title": "The Godfather Part II",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/kGzFbGhp99zva6oZODW5atUtnqi.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "In the continuing saga of the Corleone crime family, a young Vito Corleone grows up in Sicily and in 1910s New York. In the 1950s, Michael Corleone attempts to expand the family business into Las Vegas, Hollywood and Cuba.",
      "posterPath": "https://image.tmdb.org/t/p/original/bMadFzhjy9T7R8J48QGq1ngWNAK.jpg",
      "voteAverage": 8.6,
      "voteCount": 10966,
      "id": 240,
      "mediaType": 1,
      "popularity": 50.449
    },
    {
      "adult": false,
      "originalTitle": "दिलवाले दुल्हनिया ले जायेंगे",
      "releaseDate": "1995-10-20T00:00:00",
      "title": "Dilwale Dulhania Le Jayenge",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/vI3aUGTuRRdM7J78KIdW98LdxE5.jpg",
      "genreIds": [
        35,
        18,
        10749
      ],
      "originalLanguage": "hi",
      "overview": "Raj is a rich, carefree, happy-go-lucky second generation NRI. Simran is the daughter of Chaudhary Baldev Singh, who in spite of being an NRI is very strict about adherence to Indian values. Simran has left for India to be married to her childhood fiancé. Raj leaves for India with a mission at his hands, to claim his lady love under the noses of her whole family. Thus begins a saga.",
      "posterPath": "https://image.tmdb.org/t/p/original/ktejodbcdCPXbMMdnpI9BUxW6O8.jpg",
      "voteAverage": 8.6,
      "voteCount": 4162,
      "id": 19404,
      "mediaType": 1,
      "popularity": 23.182
    },
    {
      "adult": false,
      "originalTitle": "Schindler's List",
      "releaseDate": "1993-12-15T00:00:00",
      "title": "Schindler's List",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/zb6fM1CX41D9rF9hdgclu0peUmy.jpg",
      "genreIds": [
        18,
        36,
        10752
      ],
      "originalLanguage": "en",
      "overview": "The true story of how businessman Oskar Schindler saved over a thousand Jewish lives from the Nazis while they worked as slaves in his factory during World War II.",
      "posterPath": "https://image.tmdb.org/t/p/original/sF1U4EUQS8YHUYjNl3pMGNIQyr0.jpg",
      "voteAverage": 8.6,
      "voteCount": 14223,
      "id": 424,
      "mediaType": 1,
      "popularity": 42.91
    },
    {
      "adult": false,
      "originalTitle": "12 Angry Men",
      "releaseDate": "1957-04-10T00:00:00",
      "title": "12 Angry Men",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qqHQsStV6exghCM7zbObuYBiYxw.jpg",
      "genreIds": [
        18
      ],
      "originalLanguage": "en",
      "overview": "The defense and the prosecution have rested and the jury is filing into the jury room to decide if a young Spanish-American is guilty or innocent of murdering his father. What begins as an open and shut case soon becomes a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other.",
      "posterPath": "https://image.tmdb.org/t/p/original/ow3wq89wM8qd5X7hWKxiRfsFf9C.jpg",
      "voteAverage": 8.5,
      "voteCount": 7377,
      "id": 389,
      "mediaType": 1,
      "popularity": 38.807
    },
    {
      "adult": false,
      "originalTitle": "千と千尋の神隠し",
      "releaseDate": "2001-07-20T00:00:00",
      "title": "Spirited Away",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/Ab8mkHmkYADjU7wQiOkia9BzGvS.jpg",
      "genreIds": [
        16,
        10751,
        14
      ],
      "originalLanguage": "ja",
      "overview": "A young girl, Chihiro, becomes trapped in a strange new world of spirits. When her parents undergo a mysterious transformation, she must call upon the courage she never knew she had to free her family.",
      "posterPath": "https://image.tmdb.org/t/p/original/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg",
      "voteAverage": 8.5,
      "voteCount": 14501,
      "id": 129,
      "mediaType": 1,
      "popularity": 72.421
    },
    {
      "adult": false,
      "originalTitle": "Spider-Man: Across the Spider-Verse",
      "releaseDate": "2023-05-31T00:00:00",
      "title": "Spider-Man: Across the Spider-Verse",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/4HodYYKEIsGOdinkGi2Ucz6X9i0.jpg",
      "genreIds": [
        28,
        12,
        16,
        878
      ],
      "originalLanguage": "en",
      "overview": "After reuniting with Gwen Stacy, Brooklyn’s full-time, friendly neighborhood Spider-Man is catapulted across the Multiverse, where he encounters the Spider Society, a team of Spider-People charged with protecting the Multiverse’s very existence. But when the heroes clash on how to handle a new threat, Miles finds himself pitted against the other Spiders and must set out on his own to save those he loves most.",
      "posterPath": "https://image.tmdb.org/t/p/original/8Vt6mWEReuy4Of61Lnj5Xj704m8.jpg",
      "voteAverage": 8.5,
      "voteCount": 2052,
      "id": 569094,
      "mediaType": 1,
      "popularity": 1493.297
    },
    {
      "adult": false,
      "originalTitle": "君の名は。",
      "releaseDate": "2016-08-26T00:00:00",
      "title": "Your Name.",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/dIWwZW7dJJtqC6CgWzYkNVKIUm8.jpg",
      "genreIds": [
        10749,
        16,
        18
      ],
      "originalLanguage": "ja",
      "overview": "High schoolers Mitsuha and Taki are complete strangers living separate lives. But one night, they suddenly switch places. Mitsuha wakes up in Taki’s body, and he in hers. This bizarre occurrence continues to happen randomly, and the two must adjust their lives around each other.",
      "posterPath": "https://image.tmdb.org/t/p/original/q719jXXEzOoYaps6babgKnONONX.jpg",
      "voteAverage": 8.5,
      "voteCount": 9988,
      "id": 372058,
      "mediaType": 1,
      "popularity": 75.256
    },
    {
      "adult": false,
      "originalTitle": "기생충",
      "releaseDate": "2019-05-30T00:00:00",
      "title": "Parasite",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/hiKmpZMGZsrkA3cdce8a7Dpos1j.jpg",
      "genreIds": [
        35,
        53,
        18
      ],
      "originalLanguage": "ko",
      "overview": "All unemployed, Ki-taek's family takes peculiar interest in the wealthy and glamorous Parks for their livelihood until they get entangled in an unexpected incident.",
      "posterPath": "https://image.tmdb.org/t/p/original/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg",
      "voteAverage": 8.5,
      "voteCount": 15945,
      "id": 496243,
      "mediaType": 1,
      "popularity": 65.796
    },
    {
      "adult": false,
      "originalTitle": "The Green Mile",
      "releaseDate": "1999-12-10T00:00:00",
      "title": "The Green Mile",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/l6hQWH9eDksNJNiXWYRkWqikOdu.jpg",
      "genreIds": [
        14,
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "A supernatural tale set on death row in a Southern prison, where gentle giant John Coffey possesses the mysterious power to heal people's ailments. When the cell block's head guard, Paul Edgecomb, recognizes Coffey's miraculous gift, he tries desperately to help stave off the condemned man's execution.",
      "posterPath": "https://image.tmdb.org/t/p/original/o0lO84GI7qrG6XFvtsPOSV7CTNa.jpg",
      "voteAverage": 8.5,
      "voteCount": 15564,
      "id": 497,
      "mediaType": 1,
      "popularity": 56.178
    },
    {
      "adult": false,
      "originalTitle": "The Dark Knight",
      "releaseDate": "2008-07-14T00:00:00",
      "title": "The Dark Knight",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/dqK9Hag1054tghRQSqLSfrkvQnA.jpg",
      "genreIds": [
        18,
        28,
        80,
        53
      ],
      "originalLanguage": "en",
      "overview": "Batman raises the stakes in his war on crime. With the help of Lt. Jim Gordon and District Attorney Harvey Dent, Batman sets out to dismantle the remaining criminal organizations that plague the streets. The partnership proves to be effective, but they soon find themselves prey to a reign of chaos unleashed by a rising criminal mastermind known to the terrified citizens of Gotham as the Joker.",
      "posterPath": "https://image.tmdb.org/t/p/original/qJ2tW6WMUDux911r6m7haRef0WH.jpg",
      "voteAverage": 8.5,
      "voteCount": 29991,
      "id": 155,
      "mediaType": 1,
      "popularity": 79.414
    },
    {
      "adult": false,
      "originalTitle": "Cuando Sea Joven",
      "releaseDate": "2022-09-14T00:00:00",
      "title": "Cuando Sea Joven",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/ejnlCzBd5pOGAYCpxC93NXSrdrz.jpg",
      "genreIds": [
        35,
        14
      ],
      "originalLanguage": "es",
      "overview": "70-year-old Malena gets a second chance at life when she magically turns into her 22-year-old self. Now, posing as \"Maria\" to hide her true identity, she becomes the lead singer of her grandson's band and tries to recover her dream of singing, which she had to give up at some point.",
      "posterPath": "https://image.tmdb.org/t/p/original/6gIJuFHh5Lj4dNaPG3TzIMl7L68.jpg",
      "voteAverage": 8.5,
      "voteCount": 243,
      "id": 772071,
      "mediaType": 1,
      "popularity": 13.533
    },
    {
      "adult": false,
      "originalTitle": "Pulp Fiction",
      "releaseDate": "1994-09-10T00:00:00",
      "title": "Pulp Fiction",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/suaEOtk1N1sgg2MTM7oZd2cfVp3.jpg",
      "genreIds": [
        53,
        80
      ],
      "originalLanguage": "en",
      "overview": "A burger-loving hit man, his philosophical partner, a drug-addled gangster's moll and a washed-up boxer converge in this sprawling, comedic crime caper. Their adventures unfurl in three stories that ingeniously trip back and forth in time.",
      "posterPath": "https://image.tmdb.org/t/p/original/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg",
      "voteAverage": 8.5,
      "voteCount": 25383,
      "id": 680,
      "mediaType": 1,
      "popularity": 68.994
    },
    {
      "adult": false,
      "originalTitle": "Il buono, il brutto, il cattivo",
      "releaseDate": "1966-12-23T00:00:00",
      "title": "The Good, the Bad and the Ugly",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/eoCSp75lxatmIa6aGqfnzwtbttd.jpg",
      "genreIds": [
        37
      ],
      "originalLanguage": "it",
      "overview": "While the Civil War rages on between the Union and the Confederacy, three men – a quiet loner, a ruthless hitman, and a Mexican bandit – comb the American Southwest in search of a strongbox containing $200,000 in stolen gold.",
      "posterPath": "https://image.tmdb.org/t/p/original/bX2xnavhMYjWDoZp1VM6VnU1xwe.jpg",
      "voteAverage": 8.5,
      "voteCount": 7495,
      "id": 429,
      "mediaType": 1,
      "popularity": 51.484
    },
    {
      "adult": false,
      "originalTitle": "Forrest Gump",
      "releaseDate": "1994-06-23T00:00:00",
      "title": "Forrest Gump",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qdIMHd4sEfJSckfVJfKQvisL02a.jpg",
      "genreIds": [
        35,
        18,
        10749
      ],
      "originalLanguage": "en",
      "overview": "A man with a low IQ has accomplished great things in his life and been present during significant historic events—in each case, far exceeding what anyone imagined he could do. But despite all he has achieved, his one true love eludes him.",
      "posterPath": "https://image.tmdb.org/t/p/original/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg",
      "voteAverage": 8.5,
      "voteCount": 24899,
      "id": 13,
      "mediaType": 1,
      "popularity": 63.661
    },
    {
      "adult": false,
      "originalTitle": "The Lord of the Rings: The Return of the King",
      "releaseDate": "2003-12-01T00:00:00",
      "title": "The Lord of the Rings: The Return of the King",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/2u7zbn8EudG6kLlBzUYqP8RyFU4.jpg",
      "genreIds": [
        12,
        14,
        28
      ],
      "originalLanguage": "en",
      "overview": "Aragorn is revealed as the heir to the ancient kings as he, Gandalf and the other members of the broken fellowship struggle to save Gondor from Sauron's forces. Meanwhile, Frodo and Sam take the ring closer to the heart of Mordor, the dark lord's realm.",
      "posterPath": "https://image.tmdb.org/t/p/original/rCzpDGLbOoPwLjy3OAm5NUPOTrC.jpg",
      "voteAverage": 8.5,
      "voteCount": 21796,
      "id": 122,
      "mediaType": 1,
      "popularity": 69.535
    },
    {
      "adult": false,
      "originalTitle": "Primal: Tales of Savagery",
      "releaseDate": "2019-11-21T00:00:00",
      "title": "Primal: Tales of Savagery",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/uBZQOYZLIU9dBmd62fdzBAoropu.jpg",
      "genreIds": [
        28,
        12,
        16,
        18
      ],
      "originalLanguage": "en",
      "overview": "Genndy Tartakovsky's Primal: Tales of Savagery features a caveman and a dinosaur on the brink of extinction. Bonded by tragedy, this unlikely friendship becomes the only hope of survival.",
      "posterPath": "https://image.tmdb.org/t/p/original/9NBBkdxH0TjQEBSN2AzeE1sgsF9.jpg",
      "voteAverage": 8.5,
      "voteCount": 268,
      "id": 704264,
      "mediaType": 1,
      "popularity": 14.24
    },
    {
      "adult": false,
      "originalTitle": "GoodFellas",
      "releaseDate": "1990-09-12T00:00:00",
      "title": "GoodFellas",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/sw7mordbZxgITU877yTpZCud90M.jpg",
      "genreIds": [
        18,
        80
      ],
      "originalLanguage": "en",
      "overview": "The true story of Henry Hill, a half-Irish, half-Sicilian Brooklyn kid who is adopted by neighbourhood gangsters at an early age and climbs the ranks of a Mafia family under the guidance of Jimmy Conway.",
      "posterPath": "https://image.tmdb.org/t/p/original/aKuFiU82s5ISJpGZp7YkIr3kCUd.jpg",
      "voteAverage": 8.5,
      "voteCount": 11388,
      "id": 769,
      "mediaType": 1,
      "popularity": 38.29
    },
    {
      "adult": false,
      "originalTitle": "七人の侍",
      "releaseDate": "1954-04-26T00:00:00",
      "title": "Seven Samurai",
      "video": false,
      "backdropPath": "https://image.tmdb.org/t/p/original/qvZ91FwMq6O47VViAr8vZNQz3WI.jpg",
      "genreIds": [
        28,
        18
      ],
      "originalLanguage": "ja",
      "overview": "A samurai answers a village's request for protection after he falls on hard times. The town needs protection from bandits, so the samurai gathers six others to help him teach the people how to defend themselves, and the villagers provide the soldiers with food.",
      "posterPath": "https://image.tmdb.org/t/p/original/8OKmBV5BUFzmozIC3pPWKHy17kx.jpg",
      "voteAverage": 8.5,
      "voteCount": 3100,
      "id": 346,
      "mediaType": 1,
      "popularity": 30.408
    }
  ];

  constructor(private movieService: MovieService,
              private alertService: AlertService) {
  }

  ngOnInit(): void {
    //this.isLoading = true;
    // this.getPopularMovies();
    // this.getTopRatedMovies();
    // this.getUpcomingMovies();
  }

  getPopularMovies(): void {
    this.movieService.getPopularMovies()
      .subscribe((response: Movie[]) => {
          this.popularMovies = response;
        },
        (error) => {
          this.alertService.error(error)
        });
  }

  getTopRatedMovies(): void {
    this.movieService.getTopRatedMovies()
      .subscribe((response: Movie[]) => {
          this.topRatedMovies = response;
        },
        (error) => {
          this.alertService.error(error)
        });
  }

  getUpcomingMovies(): void {
    this.movieService.getUpcomingMovies()
      .subscribe((response: Movie[]) => {
          this.upcomingMovies = response;
          this.isLoading = false;
        },
        (error) => {
          this.alertService.error(error)
        });
  }

  getNowPlaying(): void{

  }
}
