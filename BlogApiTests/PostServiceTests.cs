using AutoMapper;
using BlogApi.Dtos.Posts;
using BlogApi.Interfaces;
using BlogApi.Models;
using BlogApi.Services;
using Moq;
using NUnit.Framework.Internal;


namespace BlogApiTests
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IMapper> _mapper;
        private PostService _postService;
        private List<Post> _posts;
        private List<GetPostsDto> _postDtos;
        private GetPostsDto _expectedDto;

        [SetUp]
        public void SetUp()
        {
            var user = new User { Id = 1 };
            _posts = new List<Post>()
            {
                new Post {Id = 1, Body = "body1", Title = "title1", User = user},
                new Post {Id = 2, Body = "body2", Title = "title2"},
                new Post {Id = 3, Body = "body3", Title = "title3"},
            };

            _postDtos = new List<GetPostsDto>
            {
                new GetPostsDto {Id = 1, Body = "body1", Title = "title1"},
                new GetPostsDto {Id = 2, Body = "body2", Title = "title2"},
                new GetPostsDto {Id = 3, Body = "body3", Title = "title3"},
            };

            _expectedDto = _postDtos[0];

            _postRepositoryMock = new Mock<IPostRepository>();
            _mapper = new Mock<IMapper>();
            _postService = new PostService(_postRepositoryMock.Object, _mapper.Object);
        }

        [Test]
        public async Task CreatePost_ValidUserId_ShouldReturnCreatedPostDtoAndCreatePost()
        {
            var createPostDto = new CreatePostsDto { Title = "title1", Body = "body1", UserId = 1 };

            _mapper.Setup(mapper => mapper.Map<Post>(createPostDto)).Returns(_posts[0]);
            _mapper.Setup(mapper => mapper.Map<GetPostsDto>(_posts[0])).Returns(_expectedDto);
            _postRepositoryMock.Setup(repo => repo.CreatePost(_posts[0], createPostDto.UserId))
                               .ReturnsAsync(_posts[0]);

            var result = await _postService.CreatePost(createPostDto);

            Assert.That(result, Is.EqualTo(_expectedDto));
            _mapper.Verify(mapper => mapper.Map<Post>(createPostDto));
            _mapper.Verify(mapper => mapper.Map<GetPostsDto>(_posts[0]));
            _postRepositoryMock.Verify(repo => repo.CreatePost(_posts[0], createPostDto.UserId));
        }

        [Test]
        public async Task CreatePost_InvalidUserId_ReturnNull()
        {
            var createPostDto = new CreatePostsDto { Title = "title1", Body = "body1", UserId = 1 };
            Post post = null!;
            _mapper.Setup(mapper => mapper.Map<Post>(createPostDto)).Returns(_posts[0]);
            _postRepositoryMock.Setup(repo => repo.CreatePost(_posts[0], createPostDto.UserId))
                               .ReturnsAsync(post);

            var result = await _postService.CreatePost(createPostDto);

            Assert.That(result, Is.Null);
            _mapper.Verify(mapper => mapper.Map<Post>(createPostDto));
            _postRepositoryMock.Verify(repo => repo.CreatePost(_posts[0], createPostDto.UserId));
        }


        [Test]
        public async Task DeletePost_ValidId_ShouldReturnGetPostDtoAndDeletePost()
        {
            int id = 1;

            _postRepositoryMock.Setup(repo => repo.DeletePost(id)).ReturnsAsync(_posts[0]);
            _mapper.Setup(mapper => mapper.Map<GetPostsDto>(_posts[0])).Returns(_expectedDto);

            var result = await _postService.DeletePost(id);

            Assert.That(result, Is.EqualTo(_expectedDto));
            _postRepositoryMock.Verify(repo => repo.DeletePost(id));
            _mapper.Verify(mapper => mapper.Map<GetPostsDto>(_posts[0]));

        }

        [Test]
        public async Task DeletePost_InValidId_ReturnNull()
        {
            int id = 1;
            Post post = null!;
            _postRepositoryMock.Setup(repo => repo.DeletePost(id)).ReturnsAsync(post);
            _mapper.Setup(mapper => mapper.Map<GetPostsDto>(_posts[0])).Returns(_expectedDto);

            var result = await _postService.DeletePost(id);

            Assert.That(result, Is.Null);
            _postRepositoryMock.Verify(repo => repo.DeletePost(id));
            _mapper.Verify(mapper => mapper.Map<GetPostsDto>(It.IsAny<Post>()));

        }

        [Test]
        public async Task GetAllPosts_WhenCalled_ReturnPostDtoList()
        {
            _postRepositoryMock.Setup(repo => repo.GetAllPosts()).ReturnsAsync(_posts);
            _mapper.Setup(mapper => mapper.Map<List<GetPostsDto>>(It.IsAny<List<Post>>()))
                   .Returns(_postDtos);

            var result = await _postService.GetAllPosts();

            Assert.That(result, Is.EqualTo(_postDtos));
            _postRepositoryMock.Verify(repo => repo.GetAllPosts());
            _mapper.Verify(mapper => mapper.Map<List<GetPostsDto>>(It.IsAny<List<Post>>()));

        }

        [Test]
        public async Task GetPaging_WhenCalled_ReturnListOfGetPostsDto()
        {
            int skip = 0;
            int take = 10;

            _postRepositoryMock.Setup(repo => repo.GetPaging(skip, take)).ReturnsAsync(_posts);
            _mapper.Setup(mapper => mapper.Map<List<GetPostsDto>>(It.IsAny<List<Post>>()))
                  .Returns(_postDtos);

            var result = await _postService.GetPaging(skip, take);

            CollectionAssert.AreEqual(result, _postDtos);
            _mapper.Verify(mapper => mapper.Map<List<GetPostsDto>>(It.IsAny<List<Post>>()));
            _postRepositoryMock.Verify(repo => repo.GetPaging(skip, take));
        }


        [Test]
        public async Task GetPaging_InValidSkipValue_ReturnNull()
        {
            int skip = -1;
            int take = 10;

            var result = await _postService.GetPaging(skip, take);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetPaging_InValidTakeValue_ReturnNull()
        {
            int skip = 1;
            int take = -10;

            var result = await _postService.GetPaging(skip, take);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetUserPosts_UserFound_RerurnListOfGetPostsDto()
        {
            int id = 1;
            _postRepositoryMock.Setup(repo => repo.GetUserPosts(id)).ReturnsAsync(_posts);
            _mapper.Setup(mapper => mapper.Map<List<GetPostsDto>>(_posts)).Returns(_postDtos);

            var result = await _postService.GetUserPosts(id);

            CollectionAssert.AreEqual(result, _postDtos);
            _postRepositoryMock.Verify(repo => repo.GetUserPosts(id));
            _mapper.Verify(mapper => mapper.Map<List<GetPostsDto>>(_posts));


        }

        [Test]
        public async Task GetUserPosts_UserNotFound_RerurnListOfGetPostsDto()
        {
            int id = 1;
            List<Post> posts = new List<Post>();
            List<GetPostsDto> expectedDto = new List<GetPostsDto>();
            _postRepositoryMock.Setup(repo => repo.GetUserPosts(id)).ReturnsAsync(posts);
            _mapper.Setup(mapper => mapper.Map<List<GetPostsDto>>(posts)).Returns(expectedDto);
           
            var result = await _postService.GetUserPosts(id);

            Assert.That(result, Is.Empty);
            _postRepositoryMock.Verify(repo => repo.GetUserPosts(id));
            _mapper.Verify(mapper => mapper.Map<List<GetPostsDto>>(posts));
        }

        [Test]
        public async Task UpdatePost_ValidId_ShouldReturnPostDtoAndUpdatePost()
        {
            var updatePost = new UpdatePostsDto { Id = 1, Title = "title1", Body = "body1" };

            _mapper.Setup(mapper => mapper.Map<Post>(updatePost)).Returns(_posts[0]);
            _mapper.Setup(mapper => mapper.Map<GetPostsDto>(_posts[0])).Returns(_expectedDto);

            _postRepositoryMock.Setup(repo => repo.UpdatePost(_posts[0])).ReturnsAsync(_posts[0]);

            //_postRepositoryMock.Verify(repo => repo.UpdatePost(It.IsAny<Post>()));

            var result = await _postService.UpdatePost(updatePost);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_expectedDto));
            _mapper.Verify(mapper => mapper.Map<Post>(updatePost));
            _mapper.Verify(mapper => mapper.Map<GetPostsDto>(_posts[0]));
            _postRepositoryMock.Verify(repo => repo.UpdatePost(_posts[0]));

        }

        [Test]
        public async Task UpdatePost_InValidId_ShouldReturnPostDtoAndUpdatePost()
        {
            var updatePost = new UpdatePostsDto { Id = 1, Title = "title", Body = "body" };
            Post post = null!;
            _postRepositoryMock.Setup(repo => repo.UpdatePost(It.IsAny<Post>())).ReturnsAsync(post);


            var result = await _postService.UpdatePost(updatePost);


            Assert.That(result, Is.Null);
        }


    }
}
