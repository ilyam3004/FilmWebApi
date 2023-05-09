export interface Movie {
  adult: boolean;
  originalTitle: string;
  releaseDate: string;
  title: string;
  video: boolean;
  backdropPath: string;
  genreIds: number[];
  originalLanguage: string;
  overview: string;
  posterPath: string;
  voteAverage: number;
  voteCount: number;
  id: number;
  mediaType: number;
  popularity: number;
}

export interface MovieDetails {
    adult: boolean;
    backdropPath: string;
    budget: number;
    genres: Genre[];
    homepage: string;
    id: number;
    imdbId: string;
    originalLanguage: string;
    originalTitle: string;
    overview: string;
    popularity: number;
    posterPath: string;
    productionCompanies: ProductionCompany[];
    productionCountries: ProductionCountry[];
    releaseDate: string;
    revenue: number;
    runtime: number;
    similar: any;
    recommendations: any;
    spokenLanguages: SpokenLanguage[];
    status: string;
    tagline: string;
    title: string;
    video: boolean;
    videos: Videos | null;
    voteAverage: number;
    voteCount: number;
  }

  export interface Genre {
    id: number;
    name: string;
  }

  export interface SpokenLanguage {
    iso_639_1: string;
    name: string;
  }

  export interface Videos {
    id: number;
    results: Result[];
  }

  export interface Result {
    id: string;
    iso_3166_1: string;
    iso_639_1: string;
    key: string;
    name: string;
    site: string;
    size: number;
    type: string;
  }

  export interface ProductionCompany {
    id: number;
    name: string;
    logoPath: string | null;
    originCountry: string;
  }

  export interface ProductionCountry {
    iso_3166_1: string;
    name: string;
  }
