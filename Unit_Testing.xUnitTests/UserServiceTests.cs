using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Testing.Models;

namespace Unit_Testing.xUnitTests
{
    public class UserServiceTests
    {
        private readonly UserService _demoService;
        private readonly Mock<IUserRepository> _mockRepository;
        public UserServiceTests()
        {
            _mockRepository = new Mock<IUserRepository>();
            _demoService=new UserService(_mockRepository.Object);
        }
        [Fact]
        public void GetUserById_ReturnsUser()
        {
            var userId = 1;
            var expectedUser = new User { Id = 1 ,Name = "John",Email = "john@example.com"};
            var result=_demoService.GetUserById(userId);
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Name, result.Name);
            Assert.Equal(expectedUser.Email, result.Email);
        }
        [Fact]
        public void GetUserById_ReturnsNullWhenUserNotFound()
        {
            var userId = 99;
            _mockRepository.Setup(repo => repo.GetUserById(userId)).Returns((User)null);
            var result= _demoService.GetUserById(userId);
            Assert.Null(result);
        }
        [Fact]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            var expectedUsers = new List<User>
            {
                new User{Id=1,Name="John",Email="john@example.com"},
                new User{Id=2,Name="James",Email="jaems@example.com"}
            };
            _mockRepository.Setup(repo => repo.GetAllUsers()).Returns(expectedUsers);
            var result= _demoService.GetAllUsers();
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count());
        }
        [Fact]
        public void AddUser_CallsRepository()
        {
            var newUser = new User { Id = 3, Name = "Sam", Email = "sam@example.com" };
            _demoService.AddUser(newUser);
            _mockRepository.Verify(repo=>repo.AddUser(newUser),Times.Once);
        }
        [Fact]
        public void UpdateUser_CallsRepository()
        {
            var updatedUser = new User { Id = 1, Name = "Updated", Email = "updated@example.com" };
            _demoService.UpdateUser(updatedUser);
            _mockRepository.Verify(repo=>repo.UpdateUser(updatedUser), Times.Once);
        }
        [Fact]
        public void DeleteUser_CallsRepository()
        {
            var userId = 1;
            _demoService.DeleteUser(userId);
            _mockRepository.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}