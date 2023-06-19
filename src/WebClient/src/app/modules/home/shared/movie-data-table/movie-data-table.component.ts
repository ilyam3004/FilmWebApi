import {Component, Input, OnInit} from '@angular/core';
import {MovieDetails} from "../../../../core/models/movie";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'movie-data-table',
  templateUrl: './movie-data-table.component.html',
  styleUrls: ['./movie-data-table.component.scss']
})
export class MovieDataTableComponent implements OnInit{
    @Input() movie: MovieDetails | undefined;
    formattedDate: string = ""

    constructor(private datePipe: DatePipe) { }

    ngOnInit() {
        this.formattedDate = this.datePipe
            .transform(this.movie?.releaseDate, 'd MMMM yyyy') || "";
    }
}
