import {Component, Input} from '@angular/core';
import {Movie} from "../../../../core/models/movie";

@Component({
  selector: 'slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss'],
})
export class SliderComponent {
  @Input() movies: Movie[] = [];
  @Input() title: string = '';
  slideConfig = {
    slidesToShow: 6,
    slidesToScroll: 6,
    autoplay: true,
    infinite: false,
    autoplaySpeed: 7000,
    pauseOnHover: true,
    dots: true,
    responsive: [
      {
        breakpoint: 992,
        settings: {
          arrows: true,
          infinite: false,
          slidesToShow: 3,
          slidesToScroll: 3
        }
      },
      {
        breakpoint: 768,
        settings: {
          arrows: true,
          infinite: false,
          slidesToShow: 2,
          slidesToScroll: 2
        }
      },
    ]
  }
}
