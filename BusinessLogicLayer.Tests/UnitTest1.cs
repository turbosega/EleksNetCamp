using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicLayer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private UserService            _userService;
        private Mock<IDatabaseService> _mockDatabaseService;

        [TestInitialize]
        public void Initialize()
        {
            _mockDatabaseService = new Mock<IDatabaseService>();
            _userService         = new UserService(_mockDatabaseService.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            _mockDatabaseService.Setup(s => s.GetUser()).Returns(new User
            {
                Name = "Mock User"
            });
            var user = _userService.GetUser();
            Assert.IsTrue(user.Name == "Mock User");
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(true);
        }
    }

    public interface IDatabaseService
    {
        User GetUser();
    }

    public class DatabaseService : IDatabaseService
    {
        public User GetUser()
        {
            return new User
            {
                Name = "Database User"
            };
        }
    }

    public class UserService
    {
        private readonly IDatabaseService _databaseService;

        public UserService(IDatabaseService databaseService) => _databaseService = databaseService;

        public User GetUser() => _databaseService.GetUser();
    }

    public class User
    {
        public string Name { get; set; }
    }
}