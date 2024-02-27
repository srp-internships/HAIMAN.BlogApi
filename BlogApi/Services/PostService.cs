
namespace BlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<GetPostsDto> CreatePost(CreatePostsDto newPost)
        {
            
            var post = await _postRepository.CreatePost(_mapper.Map<Post>(newPost), newPost.UserId);
            
            return _mapper.Map<GetPostsDto>(post);

        }

        public async Task<GetPostsDto> DeletePost(int id)
        {
            var postDto = await _postRepository.DeletePost(id);

            return _mapper.Map<GetPostsDto>(postDto);
        }

        public async Task<List<GetPostsDto>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPosts();
            
            return _mapper.Map<List<GetPostsDto>>(posts);
        }

        public async Task<List<GetPostsDto>> GetPaging(int skip, int take = 10)
        {
            if (skip < 0 || take < 0)
            {
                return null!;
            }

            var posts = await _postRepository.GetPaging(skip, take);

            return _mapper.Map<List<GetPostsDto>>(posts);
        }

        public async Task<List<GetPostsDto>> GetUserPosts(int id)
        {
            var userPosts = await _postRepository.GetUserPosts(id);

            return _mapper.Map<List<GetPostsDto>>(userPosts);

        }

        public async Task<GetPostsDto> UpdatePost(UpdatePostsDto updatePost)
        {
            var post = await _postRepository.UpdatePost(_mapper.Map<Post>(updatePost));

            return _mapper.Map<GetPostsDto>(post);
        }

        
    }
}
