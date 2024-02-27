using Moq;
using AutoMapper;
using BlogApi.Dtos.Users;
using BlogApi.Interfaces;
using BlogApi.Models;
using BlogApi.Services;

namespace BlogApiTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private UserService _userService;
        private List<User> _users;
        private List<UserGetDto> _usersDto;

        [SetUp]
        public void Setup()
        {
            _users = new List<User>
            {
                new User { Id = 1, FirstName = "Alice" },
                new User { Id = 2, FirstName = "Bob" }
            };

            _usersDto = new List<UserGetDto>
            {
                new UserGetDto { Id = 1, FirstName = "Alice" },
                new UserGetDto { Id = 2, FirstName = "Bob" }
            };


            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllUsers_WhenCalled_ReturnsMappedUserGetDtoList()
        {
            
            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(_users);
            _mapperMock.Setup(mapper => mapper.Map<List<UserGetDto>>(_users)).Returns(_usersDto);

            // Act
            var result = await _userService.GetAllUsers();

            
            Assert.That(result, Is.Not.Null);
            CollectionAssert.AreEqual(result, _usersDto);
            _mapperMock.Verify(mapper => mapper.Map<List<UserGetDto>>(It.IsAny<List<User>>()));
        }

        [Test]
        public async Task SearchUser_WhenCalled_ReturnsFilteredAndMappedUserGetDtoList()
        {
            // Arrange
            string searchValue = "Alice";
            var expectedDto = new List<UserGetDto>
            {
                new UserGetDto {Id = 1, FirstName = "Alice"}
            };

            _userRepositoryMock.Setup(repo => repo.SearchUser(searchValue)).ReturnsAsync(_users);
            _mapperMock.Setup(mapper => mapper.Map<List<UserGetDto>>(It.IsAny<List<User>>())).Returns(expectedDto);

            // Act
            var result = await _userService.SearchUser(searchValue);

            // Assert
            CollectionAssert.AreEqual(result, expectedDto);
            _mapperMock.Verify(mapper => mapper.Map<List<UserGetDto>>(It.IsAny<List<User>>()));
        }
    }
}
