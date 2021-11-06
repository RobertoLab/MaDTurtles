using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using System.Drawing;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.Dtos;
using Es.Udc.DotNet.PracticaMaD.Test;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao.Tests
{
    [TestClass]
    public class ImageDaoTest
    {
        public ImageDaoTest() { }

        private static IKernel kernel;
        private static IImageDao imageDao;
        private static ICategoryDao categoryDao;
        private static string imagesTestDir;
        private static long testUserId = 1;
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
            imageDao = kernel.Get<IImageDao>(); 
            categoryDao = kernel.Get<ICategoryDao>();
            string path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();
            path = path + "\\imagesTest";
            imagesTestDir = path;
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
        public void FindByImgId()
        {
            TestContext.WriteLine("-----------------------");
            TestContext.WriteLine(imagesTestDir);
            TestContext.WriteLine("-----------------------");

            // Get image to store as bytes
            FileStream imageAsFileStream = File.Open(imagesTestDir + "\\bmx.jpg", FileMode.Open);
            TestContext.WriteLine(imageAsFileStream.Length.ToString());
            int imageAsFileStreamLength = (int) imageAsFileStream.Length;
            TestContext.WriteLine(imageAsFileStreamLength.ToString());
            byte[] imageAsByte =  new byte[imageAsFileStreamLength];
            imageAsFileStream.Read(imageAsByte, 0, imageAsFileStreamLength);

            // Create a test category
            Category testCat = new Category();
            testCat.categoryId = testCatId;
            testCat.category = "test";
            categoryDao.Create(testCat);

            // Create the image
            Image image = new Image();
            image.title = "bmx";
            image.description = "first test image";
            image.uploadDate = System.DateTime.Now;
            image.categoryId = testCatId;
            image.path = null;
            image.userId = testUserId;

            imageDao.Create(image);

            Image imgStored = imageDao.Find(image.imgId);

            Assert.AreEqual(image.title, imgStored.title);
        }
    }
}
