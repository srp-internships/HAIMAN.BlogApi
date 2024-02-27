

namespace BlogApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _dataContext;

        public PostRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Post> CreatePost(Post newPost, int userId)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                return null;
            }

            newPost.User = user;
            _dataContext.Posts.Add(newPost);

            await _dataContext.SaveChangesAsync();

            return newPost;


        }

        public async Task<Post> DeletePost(int id)
        {
            var post = _dataContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post is null)
            {
                return null;
            }
            _dataContext.Posts.Remove(post);
            await _dataContext.SaveChangesAsync();
            
            return post;



        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _dataContext.Posts.Include(f => f.User).ToListAsync();
        }

        public async Task<List<Post>> GetPaging(int skip, int take)
        {
            return await _dataContext.Posts.Include(p => p.User).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<Post>> GetUserPosts(int id)
        {
            return await _dataContext.Posts.Where(p => p.User!.Id == id).Include(u => u.User).ToListAsync();
        }

        public async Task SavePosts(List<Post> posts)
        {
            _dataContext.Posts.AddRange(posts);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Post> UpdatePost(Post updatePost)
        {
            var post = _dataContext.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == updatePost.Id);
            if (post is null)
            {
                return null!;
            }

            post.Title = updatePost.Title;
            post.Body = updatePost.Body;

            await _dataContext.SaveChangesAsync();

            return post;

        }
    }
}
