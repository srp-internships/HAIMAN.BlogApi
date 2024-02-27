
namespace BlogApi.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetUserPosts(int id);
        Task<Post> CreatePost(Post newPost, int userId);
        Task<Post> UpdatePost(Post updatePost);
        Task<Post> DeletePost(int id);
        Task<List<Post>> GetPaging(int skip, int take);

        Task SavePosts(List<Post> posts);
    }
}
