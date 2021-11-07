using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using static Es.Udc.DotNet.Photogram.Model.Dtos.ImageConversor;


namespace Es.Udc.DotNet.Photogram.Model.ImageService.Test
{
    /// <summary>
    /// Tests for proper functionality on ImageService.
    /// </summary>
    [TestClass]
    public class ImageServiceTest
    {
        private static IKernel kernel;
        private static IImageService imageService;
        private static ICategoryDao categoryDao;
        private static string imagesTestDir;
        private static long testUserId = 1;
        private static int testCatId = 1;
        public string ImagesPathKey = "ImagesPath";
        public string ImagesTestPathKey = "ImagesTestPath";

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
            imageService = kernel.Get<IImageService>();
            categoryDao = kernel.Get<ICategoryDao>();
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
        public void StoreImagesTest()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imageTestPath = appSettings[ImagesTestPathKey];
            // Get image to store as bytes
            FileStream imageAsFileStream = File.Open(imageTestPath + "\\bmx.jpg", FileMode.Open);
            TestContext.WriteLine(imageAsFileStream.Length.ToString());
            int imageAsFileStreamLength = (int)imageAsFileStream.Length;
            TestContext.WriteLine(imageAsFileStreamLength.ToString());
            byte[] imageAsByte = new byte[imageAsFileStreamLength];
            imageAsFileStream.Read(imageAsByte, 0, imageAsFileStreamLength);

            // Create a test category
            //Category testCat = new Category();
            //testCat.category = "test";
            //categoryDao.Create(testCat);

            // Create a second test category
            //Category testCat2 = new Category();
            //testCat2.category = "test2";
            //categoryDao.Create(testCat2);
            
            // Create the image
            ImageDto imageDto = new ImageDto("bmx", "first test image"
                , testCatId, Convert.ToBase64String(imageAsByte), testUserId);
            
            Image image1 = imageService.StoreImageAsBlob(imageDto);

            // Create a second image
            ImageDto imageDto2 = new ImageDto("bmx", "second test image"
                , testCatId, Convert.ToBase64String(imageAsByte), testUserId);

            Image image2 = imageService.StoreImageAsFile(imageDto2);

            // Get the image stored in file system as byte array to compare.
            string imagesPath = appSettings[ImagesPathKey];
            FileStream fileImageStoredAsFileStream = File.Open(imagesPath 
                + "\\" +  image2.imgId.ToString() + ".jpeg", FileMode.Open);

            byte[] fileImageStoredAsMemoryStream = new byte[fileImageStoredAsFileStream.Length];
            fileImageStoredAsFileStream.Read(fileImageStoredAsMemoryStream, 0, (int) fileImageStoredAsFileStream.Length);
            fileImageStoredAsFileStream.Close();

            Assert.IsTrue(imageAsByte.SequenceEqual(fileImageStoredAsMemoryStream));
            
            imageService.DeleteImage(image1.imgId, testUserId);
            imageService.DeleteImage(image2.imgId, testUserId);

            Assert.ThrowsException<InstanceNotFoundException>(() => imageService.SearchImage(image1.imgId));
            Assert.ThrowsException<InstanceNotFoundException>(() => imageService.SearchImage(image2.imgId));
            Assert.IsFalse(File.Exists(imagesPath + "\\" + image2.imgId.ToString() + ".jpeg"));
        }

        [TestMethod()]
        public void SearchImagesByKeywordsTest()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imageTestPath = appSettings[ImagesTestPathKey];
            // Get image to store as bytes
            FileStream imageAsFileStream = File.Open(imageTestPath + "\\bmx.jpg", FileMode.Open);
            TestContext.WriteLine(imageAsFileStream.Length.ToString());
            int imageAsFileStreamLength = (int)imageAsFileStream.Length;
            TestContext.WriteLine(imageAsFileStreamLength.ToString());
            byte[] imageAsByte = new byte[imageAsFileStreamLength];
            imageAsFileStream.Read(imageAsByte, 0, imageAsFileStreamLength);

            ImageDto imageDto = new ImageDto("bmx", "first test image"
                , testCatId, Convert.ToBase64String(imageAsByte), testUserId);

            Image image1 = imageService.StoreImageAsBlob(imageDto);

            // Create a second image
            ImageDto imageDto2 = new ImageDto("bmx", "second test image"
                , testCatId, Convert.ToBase64String(imageAsByte), testUserId);

            Image image2 = imageService.StoreImageAsFile(imageDto2);

            ImageInfo image1Info = imageService.SearchImageEager(image1.imgId);
            ImageInfo image2Info = imageService.SearchImageEager(image2.imgId);

            List<ImageInfo> listBlockSecond = new List<ImageInfo>();
            listBlockSecond.Add(image2Info);
            Block<ImageInfo> blockSearchBySecond = 
                imageService.SearchByKeywords("second", 0, 1);

            List<ImageInfo> listBlockFirst = new List<ImageInfo>();
            listBlockFirst.Add(image1Info);
            Block<ImageInfo> blockSearchByFirst =
                imageService.SearchByKeywords("first", 0, 1);

            List<ImageInfo> listBlockFirstAndSecond = new List<ImageInfo>();
            listBlockFirstAndSecond.Add(image1Info);
            Block<ImageInfo> blockSearchByFirstAndSecondAndExistMoreItems =
                imageService.SearchByKeywords("first sec", 0, 1);

            for (int i = 0; i < listBlockSecond.Count; i++)
            {
                ImageInfo imageInfoList = listBlockSecond.ElementAt(i);
                ImageInfo imageInfoBlockList = blockSearchBySecond.items.ElementAt(i);
                Assert.AreEqual(imageInfoList.category, imageInfoBlockList.category);
                Assert.AreEqual(imageInfoList.categoryId, imageInfoBlockList.categoryId);
                Assert.AreEqual(imageInfoList.description, imageInfoBlockList.description);
                Assert.AreEqual(imageInfoList.imgBase64, imageInfoBlockList.imgBase64);
                Assert.AreEqual(imageInfoList.title, imageInfoBlockList.title);
                Assert.AreEqual(imageInfoList.uploadDate, imageInfoBlockList.uploadDate);
                Assert.AreEqual(imageInfoList.userId, imageInfoBlockList.userId);
                Assert.AreEqual(imageInfoList.userName, imageInfoBlockList.userName);
                CollectionAssert.AreEqual(imageInfoList.metadata, imageInfoBlockList.metadata);
            }
            Assert.IsFalse(blockSearchBySecond.existMoreItems);
            for (int i = 0; i < listBlockFirst.Count; i++)
            {
                ImageInfo imageInfoList = listBlockFirst.ElementAt(i);
                ImageInfo imageInfoBlockList = blockSearchByFirst.items.ElementAt(i);
                Assert.AreEqual(imageInfoList.category, imageInfoBlockList.category);
                Assert.AreEqual(imageInfoList.categoryId, imageInfoBlockList.categoryId);
                Assert.AreEqual(imageInfoList.description, imageInfoBlockList.description);
                Assert.AreEqual(imageInfoList.imgBase64, imageInfoBlockList.imgBase64);
                CollectionAssert.AreEqual(imageInfoList.metadata, imageInfoBlockList.metadata);
                Assert.AreEqual(imageInfoList.title, imageInfoBlockList.title);
                Assert.AreEqual(imageInfoList.uploadDate, imageInfoBlockList.uploadDate);
                Assert.AreEqual(imageInfoList.userId, imageInfoBlockList.userId);
                Assert.AreEqual(imageInfoList.userName, imageInfoBlockList.userName);
            }
            Assert.IsFalse(blockSearchBySecond.existMoreItems);
            for (int i = 0; i < listBlockFirstAndSecond.Count; i++)
            {
                ImageInfo imageInfoList = listBlockFirstAndSecond.ElementAt(i);
                ImageInfo imageInfoBlockList = blockSearchByFirstAndSecondAndExistMoreItems.items.ElementAt(i);
                Assert.AreEqual(imageInfoList.category, imageInfoBlockList.category);
                Assert.AreEqual(imageInfoList.categoryId, imageInfoBlockList.categoryId);
                Assert.AreEqual(imageInfoList.description, imageInfoBlockList.description);
                Assert.AreEqual(imageInfoList.imgBase64, imageInfoBlockList.imgBase64);
                CollectionAssert.AreEqual(imageInfoList.metadata, imageInfoBlockList.metadata);
                Assert.AreEqual(imageInfoList.title, imageInfoBlockList.title);
                Assert.AreEqual(imageInfoList.uploadDate, imageInfoBlockList.uploadDate);
                Assert.AreEqual(imageInfoList.userId, imageInfoBlockList.userId);
                Assert.AreEqual(imageInfoList.userName, imageInfoBlockList.userName);
            }
            Assert.IsTrue(blockSearchByFirstAndSecondAndExistMoreItems.existMoreItems);

            string imagePath = appSettings[ImagesPathKey];
            Assert.IsTrue(File.Exists(imagePath + "\\" + image2.path));
            TestContext.WriteLine(imagePath + "\\" + image2.path);

            // No puedes borrar porque has sacado de base de datos una entidad
            // que guarda relaciones con otras entidades
            /*long im1Id = image1.imgId, im2Id = image2.imgId;
            long im1UserId = image1.userId, im2UserId = image2.userId;
            imageService.DeleteImage(im1Id, im1UserId);
            imageService.DeleteImage(im2Id, im2UserId);

            Assert.IsFalse(File.Exists(imagePath + "\\" + image2.path));
            */    
        }
    }
}
