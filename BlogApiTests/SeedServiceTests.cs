using BlogApi.Interfaces;
using BlogApi.Models;
using BlogApi.Services;
using Moq;

namespace BlogApiTests
{
    [TestFixture]
    public class SeedServiceTests
    {  
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IJsonPlaceholder> _jsonPlaceholderMock;
        private SeedService _seedService;

        private List<User> _users;
        private List<Post> _posts;
     
        
        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _postRepositoryMock = new Mock<IPostRepository>();
            _jsonPlaceholderMock = new Mock<IJsonPlaceholder>();
            _seedService = new SeedService(_userRepositoryMock.Object, 
                                           _postRepositoryMock.Object, 
                                           _jsonPlaceholderMock.Object);
            
            _users = new List<User>()
            {
                new User() { Id = 1, FirstName = "Alice", LastName = "Lavrova"},
                new User() { Id = 2, FirstName = "Bob", LastName = "Bobrov"},
                new User() { Id = 3, FirstName = "Lola", LastName = "Stich"}
            };

            _posts = new List<Post>()
            {
                new Post() { Id = 1, Title = "title1", Body = "body1"},
                new Post() { Id = 2, Title = "title2", Body = "body2"},
                new Post() { Id = 3, Title = "title3", Body = "body3"}
            };
            
        
        }

        [Test]
        public async Task SeedDatabase_DataBaseIsEmpty_SavePostsAndUsers()
        {
            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(new List<User>());
            _postRepositoryMock.Setup(repo => repo.GetAllPosts()).ReturnsAsync(new List<Post>());

            _jsonPlaceholderMock.Setup(service => service.FetchUsers()).ReturnsAsync(_users);
            _userRepositoryMock.Setup(repo => repo.SaveUsers(_users)).Returns(Task.CompletedTask);

            _jsonPlaceholderMock.Setup(service => service.FetchPosts()).ReturnsAsync(_posts);
            _postRepositoryMock.Setup(repo => repo.SavePosts(_posts)).Returns(Task.CompletedTask);

            
            await _seedService.SeedDatabase();

            
            _userRepositoryMock.Verify(repo => repo.SaveUsers(_users), Times.Once);
            _postRepositoryMock.Verify(repo => repo.SavePosts(_posts), Times.Once);

            _jsonPlaceholderMock.Verify(jph => jph.FetchUsers());
            _jsonPlaceholderMock.Verify(jph => jph.FetchUsers());

        }

        [Test]
        public async Task SeedDatabase_DataBaseIsFull_DoesntSavePostsAndUsers()
        {
            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(_users);
            _postRepositoryMock.Setup(repo => repo.GetAllPosts()).ReturnsAsync(_posts);

            _jsonPlaceholderMock.Setup(service => service.FetchUsers()).ReturnsAsync(_users);
            _userRepositoryMock.Setup(repo => repo.SaveUsers(_users)).Returns(Task.CompletedTask);

            _jsonPlaceholderMock.Setup(service => service.FetchPosts()).ReturnsAsync(_posts);
            _postRepositoryMock.Setup(repo => repo.SavePosts(_posts)).Returns(Task.CompletedTask);


            await _seedService.SeedDatabase();


            _userRepositoryMock.Verify(repo => repo.SaveUsers(_users), Times.Never);
            _postRepositoryMock.Verify(repo => repo.SavePosts(_posts), Times.Never);

            _jsonPlaceholderMock.Verify(jph => jph.FetchUsers(), Times.Never);
            _jsonPlaceholderMock.Verify(jph => jph.FetchUsers(), Times.Never);

        }


    }
}
