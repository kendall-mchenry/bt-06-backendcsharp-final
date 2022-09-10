import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '../models/post';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  postBaseUrl: string = "https://localhost:7217/api/posts";

  tokenKey: string = "myUserToken";

  constructor(private http: HttpClient) { }

  // POST / create new post
  createPost(newPost: Post): Observable<Post> {
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
    };

    return this.http.post<Post>(`${this.postBaseUrl}`, newPost, { headers: reqHeaders });
  }

  // GET / one post by post id
  getPostById(postId: number): Observable<Post> {
    return this.http.get<Post>(`${this.postBaseUrl}/${postId}`);
  }

  // GET / all posts
  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.postBaseUrl}`);
  }

  // GET / all posts by user id
  getPostsByUserId(userId: number): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.postBaseUrl}/user/${userId}`);
  }

  // GET / ALL CURRENT user posts
  getCurrentUserPosts(userId: number): Observable<Post[]> {
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
    };

    return this.http.get<Post[]>(`${this.postBaseUrl}/user/current`,{ headers: reqHeaders });
  }

  // PUT / edit post by post id
  editPost(editPost: Post): Observable<Post> {
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
    };

    return this.http.put<Post>(`${this.postBaseUrl}/edit/${editPost.postId}`, editPost, { headers: reqHeaders });
  }

  // DELETE / post by post id
  deletePostById(postId: number): Observable<any> {
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
    };

    return this.http.delete<any>(`${this.postBaseUrl}/${postId}`, { headers: reqHeaders });
  }
}
