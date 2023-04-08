import {Component, OnInit} from '@angular/core';
import {PostService} from "./core/services/post.service";
import {Post} from "./core/models/post";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  public posts: Post[];

  constructor(private postService: PostService) {
    this.posts = [];
  }

  ngOnInit() {
    this.getPosts();
  }

  public getPosts(): void {
    this.postService.getPosts().subscribe(
      (response: Post[]) => {
        this.posts = response;
      },
      (error: HttpErrorResponse) => {
        alert(error.message);;
      }
    )
    console.log(this.posts)
  }
}
