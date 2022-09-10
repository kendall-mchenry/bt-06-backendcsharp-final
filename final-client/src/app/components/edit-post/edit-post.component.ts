import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.css']
})
export class EditPostComponent implements OnInit {

  // HOW TO RESTRICT THIS TO ONLY BE ALLOWED ON THE USER THAT IS SIGNED IN?

  editPost: Post = new Post();
  editPostId: number = 0;

  constructor(private postService: PostService, private router: Router, private actRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.editPostId = Number(this.actRoute.snapshot.paramMap.get('id'));
    this.postService.getPostById(this.editPostId).subscribe((findPost) => {
      this.editPost = findPost
    });
  }

  onEditSubmit() {
    this.postService.editPost(this.editPost).subscribe(() => {
      window.alert("Post has been updated");
      this.router.navigate(['profile/', this.editPost.userId]);
    }, error => {
      window.alert("Unable to update post");
      console.log("Error: ", error);
      if (error.status === 401) {
        this.router.navigate(['signin']);
      }
    });
  }

}
