namespace Artnivora.Studio.Portal.Business.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using Artnivora.Studio.Portal.Business.Services.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Object representing unit test for <see cref="SecurePasswordHasher"/>
    /// </summary>
    [TestClass]
    public class SecurePasswordHasherUnitTest
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
        }

        /// <summary>
        /// Tests the hashing of password.
        /// </summary>
        [TestMethod]
        public void TestHashPassword()
        {

            //9636f7f7599e04860c985e74ef7375f5f708f61c33ad3f2ca2dc6facb50df3cd
            var password = "P@ssword01!";
            var hashedPassword = SecurePasswordHasher.Hash(password);

            Assert.IsTrue(SecurePasswordHasher.Verify(password, hashedPassword));
        }

        /// <summary>
        /// Tests the hashing of SHA256 password.
        /// </summary>
        [TestMethod]
        public void TestHashOfSHA256Password()
        {
            string passwordHashed = ShaHasher.Hash("@dm!n123");
            var hashedPassword = SecurePasswordHasher.Hash(passwordHashed);

            Assert.IsTrue(SecurePasswordHasher.Verify(passwordHashed, hashedPassword));
        }
    }
}