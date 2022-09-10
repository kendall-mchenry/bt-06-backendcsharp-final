import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  username: string = '';
  password: string = '';

  signedInUser: User = new User();

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {

  }

  signin() {
    // Add error handling to make sure username & password are valid inputs?

    this.userService.signin(this.username, this.password).subscribe((response: any) => {
      // How would I navigate to that user's page upon login?
      this.router.navigateByUrl('/posts').then(() => {
        window.location.reload();
      });
    }, error => {
      console.log('Error: ', error);
      window.alert("Unsuccessful Login");
      this.router.navigateByUrl('/signin');
    });
  }

}
