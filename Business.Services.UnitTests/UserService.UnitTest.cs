namespace Artnivora.Studio.Portal.Business.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Object representing unit test for <see cref="UserService"/>
    /// </summary>
    [TestClass]
    public class UserServiceUnitTest
    {
        private Mock<IUserDataService> _mockUserDataService;
        private Models.User _userModelToBeAdd;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var listOfUsers = new List<Models.User>();
            listOfUsers.Add(new Models.User()
            {
                Id = new Guid("{EB2BF836-CE37-4A98-A442-692025D2F606}"),
                Username = "user1",
                Password = "SomePassword01!",
                Email = "user1@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test1"
            });
            listOfUsers.Add(new Models.User()
            {
                Id = new Guid("{0B6F75BD-E16C-43D4-8DF4-A0F5646C0D0F}"),
                Username = "user2",
                Password = "SomePassword02!",
                Email = "user2@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test2"
            });
            listOfUsers.Add(new Models.User()
            {
                Id = new Guid("{3FF4FD8C-5F1E-4A98-9151-798ECEBE2343}"),
                Username = "user3",
                Password = "SomePassword03!",
                Email = "user3@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test3"
            });

            _userModelToBeAdd = new Models.User()
            {
                Id = new Guid("{5E27FD30-7888-4606-895B-4B48140C5270}"),
                Username = "user4",
                Password = "SomePassword04!",
                Email = "user4@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test4"
            };

            _mockUserDataService = new Mock<IUserDataService>();
            _mockUserDataService.Setup(us => us.GetAll()).Returns(listOfUsers);
            _mockUserDataService.Setup(us => us.GetById(new Guid("{EB2BF836-CE37-4A98-A442-692025D2F606}"))).Returns(listOfUsers[0]);
            _mockUserDataService.Setup(us => us.GetById(new Guid("{0B6F75BD-E16C-43D4-8DF4-A0F5646C0D0F}"))).Returns(listOfUsers[1]);
            _mockUserDataService.Setup(us => us.GetById(new Guid("{3FF4FD8C-5F1E-4A98-9151-798ECEBE2343}"))).Returns(listOfUsers[2]);
            _mockUserDataService.Setup(us => us.GetByUsername("user1")).Returns(listOfUsers[0]);
            _mockUserDataService.Setup(us => us.GetByUsername("user2")).Returns(listOfUsers[1]);
            _mockUserDataService.Setup(us => us.GetByUsername("user3")).Returns(listOfUsers[2]);
            _mockUserDataService.Setup(us => us.Add(_userModelToBeAdd));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _mockUserDataService = null;
        }

        /// <summary>
        /// Tests the get all users.
        /// </summary>
        [TestMethod]
        public void TestGetAllUsers()
        {
            using(var userService = new UserService(_mockUserDataService.Object))
            {
                var users = new List<Models.User>(userService.GetAll());
                Assert.IsTrue(users.Count == 3);
            }
        }

        /// <summary>
        /// Tests the get user by identifier.
        /// </summary>
        [TestMethod]
        public void TestGetUserById()
        {
            using (var userService = new UserService(_mockUserDataService.Object))
            {
                bool checks = true;
                var user = userService.GetById(new Guid("{EB2BF836-CE37-4A98-A442-692025D2F606}"));
                checks = (user.Username == "user1");

                var user2 = userService.GetById(Guid.NewGuid());
                checks = (user2 == null);
                
                Assert.IsTrue(checks);
            }
        }

        /// <summary>
        /// Tests the save of a new user.
        /// </summary>
        [TestMethod]
        public void TestSave()
        {
            using (var userService = new UserService(_mockUserDataService.Object))
            {
                User user = userService.Save(_userModelToBeAdd);
				Assert.IsTrue(user != null);
            }
        }
    }
}