using Newtonsoft.Json;

namespace BlogApi.Services
{
    public class SeedService : ISeedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IJsonPlaceholder _jsonPlaceholder;
        public SeedService(IUserRepository userRepository, IPostRepository postRepository, IJsonPlaceholder jsonPlaceholder)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _jsonPlaceholder = jsonPlaceholder;
        }
        public async Task SeedDatabase()
        {
            var users = await _userRepository.GetAllUsers();
            var posts = await _postRepository.GetAllPosts();


            if (users.Count == 0 || posts.Count == 0)
            {
                var userList = await _jsonPlaceholder.FetchUsers();
                await _userRepository.SaveUsers(userList);

                var postsList = await _jsonPlaceholder.FetchPosts();
                await _postRepository.SavePosts(postsList);
            }
        }
    }
}

