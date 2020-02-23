namespace Artnivora.Studio.Portal.Business.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Object representing unit test of <see cref="ExtensionMethods"/>
    /// </summary>
    [TestClass]
    public class ExtensionMethodsUnitTest
    {
        private List<Models.User> _listOfUsers;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _listOfUsers = new List<Models.User>();
            _listOfUsers.Add(new Models.User()
            {
                Id = new Guid("{EB2BF836-CE37-4A98-A442-692025D2F606}"),
                Username = "user1",
                Password = "SomePassword01!",
                Email = "user1@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test1"
            });
            _listOfUsers.Add(new Models.User()
            {
                Id = new Guid("{0B6F75BD-E16C-43D4-8DF4-A0F5646C0D0F}"),
                Username = "user2",
                Password = "SomePassword02!",
                Email = "user2@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test2"
            });
            _listOfUsers.Add(new Models.User()
            {
                Id = new Guid("{3FF4FD8C-5F1E-4A98-9151-798ECEBE2343}"),
                Username = "user3",
                Password = "SomePassword03!",
                Email = "user3@somedomain.local",
                CreationDate = DateTime.Now,
                Token = "test3"
            });
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
        }

        /// <summary>
        /// Tests the create without passwords.
        /// </summary>
        [TestMethod]
        public void TestCreateWithoutPasswords()
        {
            var users = ExtensionMethods.WithoutPasswords(_listOfUsers);
            bool checks = true;
            foreach(var user in users)
            {
                checks = (user.Password == null);
            }
            Assert.IsTrue(checks);
        }
    }
}
