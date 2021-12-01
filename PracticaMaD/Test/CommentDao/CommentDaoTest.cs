using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Test;

namespace Es.Udc.DotNet.Photogram.Model.CommentDao.Tests
{
    [TestClass]
    public class CommentDaoTest
    {
        public CommentDaoTest() { }

        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static IImageDao imageDao;
        private static IUserDao userDao;
        private static int testCatId = 1;

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
            commentDao = kernel.Get<ICommentDao>();
            imageDao = kernel.Get<IImageDao>();
            userDao = kernel.Get<IUserDao>();
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

        #endregion


        /// <summary>
        /// Test create images, find image by id, find images by keywords.
        /// </summary>
        [TestMethod]
        public void CommentDaoMethodsTest()
        {
            long testUserId = 1;
            long testImgId = 1;
            // Store image for testing
            Image image = new Image();
            image.title = "bmx";
            image.description = "first test image";
            image.uploadDate = System.DateTime.Now;
            image.categoryId = testCatId;
            image.path = null;
            image.userId = testUserId;
            image.img = null;

            imageDao.Create(image);

            User newUser = new User();
            newUser.userName = "jsmith";
            newUser.password = "password";
            newUser.firstName = "John";
            newUser.lastName1 = "Smith";
            newUser.lastName2 = "Smith";
            newUser.email = "jsmith@acme.com";
            newUser.language = "en";
            newUser.country = "US";

            userDao.Create(newUser);

            testImgId = image.imgId;
            testUserId = newUser.userId;

            bool userExists = userDao.Exists(newUser.userId);

            Assert.IsTrue(userExists);
            // Create the comments
            Comment comment = new Comment();
            comment.userId = testUserId;
            comment.imgId = testImgId;
            comment.uploadDate = DateTime.Now;
            comment.comment = "Test comment 1";
            commentDao.Create(comment);

            Comment comment2 = new Comment();
            comment2.userId = testUserId;
            comment2.imgId = testImgId;
            comment2.uploadDate = DateTime.Now;
            comment2.comment = "Test comment 2";
            commentDao.Create(comment2);

            // Check that the image with id 1 is commented
            bool imgCommented =commentDao.IsCommented(testImgId);
            //bool imgCommented2 = commentDao.IsCommented(2);
            Assert.IsTrue(imgCommented);
            //Assert.IsFalse(imgCommented2);


            //Check that the comments save well

            List<Comment> comments = new List<Comment>();

            comments.Add(comment);
            comments.Add(comment2);

            List<Comment> imgComments = commentDao.GetCommentsFromImage(testImgId);

            CollectionAssert.AreEqual(comments, imgComments);
        }
    }
}

