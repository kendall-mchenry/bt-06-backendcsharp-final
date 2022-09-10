import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditPostComponent } from './components/edit-post/edit-post.component';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { PostFeedComponent } from './components/post-feed/post-feed.component';
import { SearchUsersComponent } from './components/search-users/search-users.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  { path: '', redirectTo: 'posts', pathMatch: 'full' },
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'posts', component: PostFeedComponent },
  { path: 'posts/edit/:id', component: EditPostComponent },
  { path: 'profile/:id', component: UserProfileComponent },
  { path: 'profile/users/all', component: SearchUsersComponent },
  { path: 'profile/edit/current', component: EditUserComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
