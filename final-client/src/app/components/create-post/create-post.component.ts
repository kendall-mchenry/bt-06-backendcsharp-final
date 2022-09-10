import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  newPost: Post = new Post();

  constructor(private postService: PostService, private router: Router) { }

  ngOnInit(): void {
  }

  // why is this showing if they're signed in or not?
  addPost() {
    this.postService.createPost(this.newPost).subscribe(() => {
      window.alert("New Post Created Successfully");
      window.location.reload();
    }, error => {
      console.log('Error: ', error);
      if (error.status === 401 || error.status === 403) {
        this.router.navigate(['signin']);
      }
    });
  }

}
