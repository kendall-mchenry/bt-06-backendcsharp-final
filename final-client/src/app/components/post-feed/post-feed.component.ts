import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post';
import { User } from 'src/app/models/user';
import { PostService } from 'src/app/services/post.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-post-feed',
  templateUrl: './post-feed.component.html',
  styleUrls: ['./post-feed.component.css']
})
export class PostFeedComponent implements OnInit {

  postList: Post[] = [];

  // currentPost: Post = new Post();

  userList: User[] = [];

  // postUser: User = new User();
  // postUserId?: number = 0;

  currentUser: User = new User();
  // currentUserId?: number = 0;

  // postUsername?: string = "";

  constructor(private postService: PostService, private userService: UserService) { }

  ngOnInit(): void {
    this.postService.getAllPosts().subscribe(posts => {
      this.postList = posts;
    });

    this.userService.getAllUsers().subscribe(users => {
      this.userList = users;
    });

    this.userService.getCurrentUser().subscribe(user => {
      this.currentUser = user;
    });

       

    // this.currentUserId = this.currentUser.userId;
    
    // loop through every item in the postlist
    // for (let post of this.postList) {
    //   this.postUserId = post.userId;

    //   if (this.postUserId == this.currentUserId) {
    //     this.postUsername = this.currentUser.username;
    //   } else {
    //     // OR do I need to find the active user in the user list?
    //     this.userService.getUserById(this.postUserId!).subscribe(user => {
    //       this.postUser = user;
    //     });
        
    //     this.postUsername = this.postUser.username;
    //   }
    // }
    
  }

  // getPostUserById(id: number): number {
  //   this.userService.getUserById(id).subscribe(user => {
  //     this.postUser = user;
  //   });

  //   this.postUserId = this.postUser.userId!;

  //   return this.postUserId;
  // }


  // also add created date/time to the post
  // add photo to the post OR just to the user profile page?
  
  // getPostUser(postId: number) {
  //   this.postService.getPostById(postId).subscribe(post => {
  //     this.currentPost = post;
  //   });

  //   this.postUserId = this.currentPost.userId;
    
  //   this.userService.getUserById(this.postUserId!).subscribe(user => {
  //     this.postUser = user;
  //   });

  //   this.postUsername = this.postUser.username;

  // }
  
}
