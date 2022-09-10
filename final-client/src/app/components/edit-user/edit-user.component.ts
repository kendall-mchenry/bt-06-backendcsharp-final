import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  currentUser: User = new User();
  editUserId: number = 0;

  constructor(private userService: UserService, private router: Router, private actRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.userService.getCurrentUser().subscribe(user => {
      this.currentUser = user;
      console.log(`Edit user is: ${this.currentUser.username}`)
    });
    
  }

  // WHY ISN'T THIS WORKING?
  onEditSubmit() {
    this.userService.editCurrentUser(this.currentUser).subscribe(() => {
      window.alert("User has been updated.");
      this.router.navigate(['profile/', this.currentUser.userId]);
    }, error => {
      window.alert("Unable to update user.");
      console.log("Error: ", error);
      if (error.status === 401) {
        this.router.navigate(['signin']);
      }
    });
  }

}
