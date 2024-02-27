using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetPostsDto>>> GetAll()
        {
            return Ok(await _postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Post>>> GetUsersPosts(int id)
        {
            var post = await _postService.GetUserPosts(id);
            if (post.Count == 0)
            {
                return NotFound($"The user with Id: {id} was not found!");
            }
            return Ok(post);
        }


        [HttpGet("GetPaging/{skip}/{take}")]
        public async Task<ActionResult<List<GetPostsDto>>> GetPaging(int skip, int take = 10)
        {
            var posts = await _postService.GetPaging(skip, take);
            if (posts is null)
            {
                return BadRequest();
            }
            return Ok(posts);
        }


        [HttpPut]
        public async Task<ActionResult<GetPostsDto>> UpdatePost(UpdatePostsDto updatePost)
        {
            var post = await _postService.UpdatePost(updatePost);
            if (post is null)
            {
                return NotFound($"The post with Id: {updatePost.Id} was not found!");
            }
            return Ok(post);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _postService.DeletePost(id);
            if (post is null)
            {
                return NotFound($"The post with Id: {id} was not found!");
            }
            return Ok("The post was deleted");
        }

        [HttpPost("CreatePost")]
        public async Task<ActionResult<GetPostsDto>> CreateNewPost(CreatePostsDto newPost)
        {
            var post = await _postService.CreatePost(newPost);
            if (post is null)
            {
                return NotFound($"The user with Id: {newPost.UserId} was not found!");
            }
            return Ok(post);
        }






    }
}
