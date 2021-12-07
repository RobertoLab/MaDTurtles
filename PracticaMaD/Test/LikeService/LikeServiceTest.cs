using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.LikeService;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;


namespace Es.Udc.DotNet.Photogram.Model.LikeService.Test
{
    /// <summary>
    /// Tests for proper functionality on ImageService.
    /// </summary>
    [TestClass]
    public class LikeServiceTest
    {
        private static IKernel kernel;
        private static ILikeService likeService;
        private static IUserDao userDao;
        private static IImageDao imageDao;

        private TestContext testContextInstance;

        private TransactionScope transactionScope;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales

        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            likeService = kernel.Get<ILikeService>();
            userDao = kernel.Get<IUserDao>();
            imageDao = kernel.Get<IImageDao>();
        }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        private User signUpUser(string userName)
        {
            User user = new User();
            user.userName = userName;
            user.password = "password";
            user.firstName = "John";
            user.lastName1 = "Smith";
            user.lastName2 = "Smith";
            user.email = "test@acme.com";
            user.language = "en";
            user.country = "US";

            userDao.Create(user);
            return user;
        }

        private Image uploadImage(long userUploadId)
        {
            Image image = new Image();
            image.title = "bmx";
            image.description = "first test image";
            image.uploadDate = System.DateTime.Now;
            image.categoryId = 1;
            image.path = null;
            image.userId = userUploadId;
            image.img = null;

            imageDao.Create(image);
            return image;
        }

        #endregion
        [TestMethod]
        public void LikeServiceMethodsTest()
        {
            User user1 = signUpUser("test1");
            User user2 = signUpUser("test2");

            Image image1 = uploadImage(user1.userId);

            Assert.AreEqual(0,likeService.GetImageLikes(image1.imgId));

            likeService.LikeImage( user2.userId, image1.imgId);
            Assert.AreEqual(1, likeService.GetImageLikes(image1.imgId));

            likeService.LikeImage(user1.userId, image1.imgId);
            Assert.AreEqual(2, likeService.GetImageLikes(image1.imgId));

            likeService.Unlike(user2.userId, image1.imgId);
            Assert.AreEqual(1, likeService.GetImageLikes(image1.imgId));
        }

    }
}