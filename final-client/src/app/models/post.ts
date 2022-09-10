import { User } from "./user";

export class Post {

    // Make this match API models
    postId?: number;
    userId?: User["userId"];
    user?: User;
    title?: string;
    content?: string;
    postedDate?: string;

    constructor(postId?: number, userId?: User["userId"], user?: User, title?: string, content?: string, postedDate?: string) 
    {
        this.postId = postId;
        this.userId = userId;
        this.user = user;
        this.title = title;
        this.content = content;
        this.postedDate = postedDate;
    }
    
}