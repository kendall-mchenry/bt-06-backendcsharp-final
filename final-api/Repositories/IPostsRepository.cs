using final_api.Models;

namespace final_api.Repositories;

public interface IPostsRepository
{
    IEnumerable<Post> GetAllPosts();

    Post? GetPostByPostId(int postId);

    // Do these go in this repo OR the user repo? (get the LIST of posts by USER ID)
    List<Post>? GetPostsByUserId(int userId);
    // Post? GetPostByUserId(int userId);

    Post CreatePost(Post newPost);

    Post? EditPost(Post editPost);

    void DeletePostById(int postId);

}