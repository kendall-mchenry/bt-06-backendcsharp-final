import { Post } from "./post";

export class User {

    // Model must match API model
    userId?: number; // required, set upon creation
    username?: string; // required
    password?: string; // required
    firstName?: string;
    lastName?: string;
    state?: string;
    photoUrl?: string;
    createdDate?: string;

    posts?: Array<Post>; // JSON ignore

    constructor(id?: number, username?: string, password?: string, firstName?: string, lastName?: string, state?: string, photoUrl?: string, createdDate?: string, posts?: Array<Post>) 
    {
        this.userId = id;
        this.username = username;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.state = state;
        this.photoUrl = photoUrl;
        this.createdDate = createdDate;
        this.posts = posts;
    }

}

