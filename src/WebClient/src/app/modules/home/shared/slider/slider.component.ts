import {Component, Input} from '@angular/core';
import {Movie} from "../../../../core/models/movie";
import {SlickCarouselComponent, SlickCarouselModule} from "ngx-slick-carousel";

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
    autoplaySpeed: 5000,
    pauseOnHover: true,
    infinite: true,
    dots: true,
    responsive: [
      {
        breakpoint: 992,
        settings: {
          arrows: true,
          infinite: true,
          slidesToShow: 4,
          slidesToScroll: 4
        }
      },
      {
        breakpoint: 768,
        settings: {
          arrows: true,
          infinite: true,
          slidesToShow: 3,
          slidesToScroll: 3
        }
      },
    ]
  }
}
