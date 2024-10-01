using System.Collections;
using System.Collections.Generic;
using Unit_Testing.Models;
namespace Unit_Testing.xUnitTests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        public UserRepositoryTests()
        {
            _userRepository = new UserRepository();
        }
        [Fact]
        public void GetUserById_ReturnsCorrectUser()
        {
            var userId = 1;
            var result=_userRepository.GetUserById(userId);
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }
        [Theory]
        [ClassData(typeof(GetUserByIdTestData))]
        //[InlineData(1,true)]
        //[InlineData(99,false)]
        public void GetUserById_ReturnsExpectedResult(int userId,bool userExists)
        {
            var result = _userRepository.GetUserById(userId);
            if(userExists)
            {
                Assert.NotNull(result);
                Assert.Equal(userId, result.Id);
            }
            else
            {
                Assert.Null(result);
            }
        }
        [Fact]
        public void GetUserById_ReturnsNullWhenUserNotFound()
        {
            var userId = 99;
            var result=_userRepository.GetUserById(userId);
            Assert.Null(result);
        }
        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            var result=_userRepository.GetAllUsers();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void AddUser_AddUserCorrectly()
        {
            var newUser = new User { Id = 3, Name = "Sam", Email = "sam@example.com" };
            _userRepository.AddUser(newUser);
            var result = _userRepository.GetUserById(3);
            Assert.NotNull(result);
            Assert.Equal(newUser.Id, result.Id);
            Assert.Equal(newUser.Name, result.Name);
            Assert.Equal(newUser.Email, result.Email);
        }
        [Fact]
        public void UpdateUser_UpdateUserCorrectly()
        {
            var updatedUser = new User { Id = 1, Name = "John", Email = "john@example.com" };
            _userRepository.UpdateUser(updatedUser);
            var result = _userRepository.GetUserById(1);
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Name, result.Name);
            Assert.Equal(updatedUser.Email, result.Email);
        }
        [Fact]
        public void DeleteUser_DeleteUserCorrectly()
        {
            var userId = 1;
            _userRepository.DeleteUser(userId);
            var result = _userRepository.GetUserById(userId);
            Assert.NotNull(result);
        }
    }
    public class GetUserByIdTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Using yield return to provide test cases
            // Each test case is represented as an object array with the expected parameters and result
            yield return new object[] { 1, true }; // Test case with userId 1 and expected result true
            yield return new object[] { 99, false }; // Test case with userId 99 and expected result false
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}