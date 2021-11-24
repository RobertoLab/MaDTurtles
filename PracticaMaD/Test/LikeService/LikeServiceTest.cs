using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.LikeService;
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
            likeService = kernel.Get<ILikeService>();
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
        [TestMethod]
        public void LikeServiceMethodsTest()
        {
            Assert.Equals(0,likeService.GetImageLikes(testImgId));

            likeService.LikeImage( testUserId, testImgId);
            Assert.Equals(1, likeService.GetImageLikes(testImgId));

            likeService.LikeImage(testUserId2, testImgId);
            Assert.Equals(2, likeService.GetImageLikes(testImgId));

            likeService.Unlike(testUserId2, testImgId);
            Assert.Equals(1, likeService.GetImageLikes(testImgId));





        }

    }
}