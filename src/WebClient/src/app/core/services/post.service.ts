import {environment} from "../../../environments/environment.development";
import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Post} from '../models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  public getPosts(): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.apiBaseUrl}/posts`);
  }
}
