import {Component, Input} from '@angular/core';
import {Movie} from "../../../../core/models/movie";

@Component({
  selector: 'slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss']
})
export class SliderComponent {
  @Input() movies: Movie[] = [];
  @Input() title: string = '';
}
