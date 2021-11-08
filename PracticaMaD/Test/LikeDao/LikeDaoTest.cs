using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.LikeDao;
using Es.Udc.DotNet.Photogram.Test;

namespace Es.Udc.DotNet.Photogram.Model.LikeDao.Tests
{
    [TestClass]
    public class LikeDaoTest
    {
        public LikeDaoTest() { }

        private static IKernel kernel;
        private static ILikeDao likeDao;
        private static long testUserId = 1;
        private static long testUserId2 = 2;
        private static long testImgId = 1;
        private static long testImgId2 = 2;

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
            likeDao = kernel.Get<ILikeDao>();
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
        public void LikeDaoMethodsTest()
        {


            // Create the likes
            Like like = new Like();
            like.userId = testUserId;
            like.imgId = testImgId;
            likeDao.Create(like);

            Like like2 = new Like();
            like2.userId = testUserId2;
            like2.imgId = testImgId;
            likeDao.Create(like2);

            // Check that the image with id 1 is likeed
            bool imgLikeed = likeDao.AlreadyLiked(testImgId, testUserId);
            bool imgLikeed2 = likeDao.AlreadyLiked(testImgId, testUserId);
            Assert.IsTrue(imgLikeed);
            Assert.IsFalse(imgLikeed2);


            //Check that the likes save well


            int imgLikes = likeDao.NumberOfLikes(testImgId);

            int img2Likes = likeDao.NumberOfLikes(testImgId2);

            CollectionAssert.Equals(2, imgLikes);
            CollectionAssert.Equals(0, img2Likes);
        }
    }
}