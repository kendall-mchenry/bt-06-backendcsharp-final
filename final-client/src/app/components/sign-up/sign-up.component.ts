import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  newUser: User = new User();

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  signup()
  {
    this.userService.signup(this.newUser).subscribe(() => {
      window.alert("User Registered Successfully");
      this.router.navigate(['signin']);
    }, error => {
      window.alert("User Registration Error");
      console.log('Error: ', error);
    });
  }

}
