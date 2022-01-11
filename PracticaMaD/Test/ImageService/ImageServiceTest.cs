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
using Es.Udc.DotNet.Photogram.Model.UserDao;
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
        private static IUserDao userDao;
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

        #region Private helper functions

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
        

        private byte[] readStoredImage(string imageName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imageTestPath = appSettings[ImagesTestPathKey];
            FileStream imageAsFileStream = File.Open(imageTestPath + "\\" + imageName + ".jpg", FileMode.Open);
            int imageAsFileStreamLength = (int)imageAsFileStream.Length;
            byte[] imageAsByte = new byte[imageAsFileStreamLength];
            imageAsFileStream.Read(imageAsByte, 0, imageAsFileStreamLength);
            imageAsFileStream.Close();

            return imageAsByte;
        }
        #endregion

        [TestMethod]
        public void StoreImagesTest()
        {
            var appSettings = ConfigurationManager.AppSettings;
            // Get image to store as bytes, size < 200KB, name "bmx"
            byte[] imageAsByteToBlob = readStoredImage("bmx");

            // Get image to store as bytes, size > 200KB, name "turtle"
            byte[] imageAsByteToFile = readStoredImage("turtle");

            // Create a user 
            User user = signUpUser("test");
            long testUserId = user.userId;

            // Create the image
            ImageDto imageDto = new ImageDto("bmx", "first test image",
                testCatId, Convert.ToBase64String(imageAsByteToBlob), testUserId,
                "test testing",
                float.NaN, float.NaN, float.NaN, float.NaN);
            
            Image image1 = imageService.StoreImage(imageDto);

            // Create a second image
            ImageDto imageDto2 = new ImageDto("bmx", "second test image",
                testCatId, Convert.ToBase64String(imageAsByteToFile), testUserId,
                "test testing",
                float.NaN, float.NaN, float.NaN, float.NaN);

            Image image2 = imageService.StoreImage(imageDto2);

            // Get the image stored in file system as byte array to compare.
            string imagesPath = appSettings[ImagesPathKey];
            FileStream fileImageStoredAsFileStream = File.Open(imagesPath 
                + "\\" +  image2.imgId.ToString() + ".jpeg", FileMode.Open);

            byte[] fileImageStoredAsMemoryStream = new byte[fileImageStoredAsFileStream.Length];
            fileImageStoredAsFileStream.Read(fileImageStoredAsMemoryStream, 0, (int) fileImageStoredAsFileStream.Length);
            fileImageStoredAsFileStream.Close();

            Assert.IsTrue(imageAsByteToFile.SequenceEqual(fileImageStoredAsMemoryStream));
            
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
            // Get image to store as bytes, size < 200KB, name "bmx"
            byte[] imageAsByteToBlob = readStoredImage("bmx");

            // Get image to store as bytes, size > 200KB, name "turtle"
            byte[] imageAsByteToFile = readStoredImage("turtle");

            // Create a user 
            User user = signUpUser("test");
            long testUserId = user.userId;

            ImageDto imageDto = new ImageDto("bmx", "first test image"
                , testCatId, Convert.ToBase64String(imageAsByteToBlob), testUserId,
                "test testing",
                0, 1, 2, 3);

            Image image1 = imageService.StoreImage(imageDto);

            // Create a second image
            ImageDto imageDto2 = new ImageDto("bmx", "second test image"
                , testCatId, Convert.ToBase64String(imageAsByteToFile), testUserId,
                "test testing",
                float.NaN, float.NaN, float.NaN, float.NaN);

            Image image2 = imageService.StoreImage(imageDto2);

            ImageBasicInfo image1Info = imageService.SearchImageBasic(image1.imgId);
            ImageBasicInfo image2Info = imageService.SearchImageBasic(image2.imgId);

            imageService.ModifyImageTags(image1.imgId, "tested");
            ImageInfo imageInfoTagsTest = imageService.SearchImageEager(image1.imgId);

            Assert.IsTrue(imageInfoTagsTest.tags.Count() == 1);

            List<ImageBasicInfo> listBlockSecond = new List<ImageBasicInfo>();
            listBlockSecond.Add(image2Info);
            Block<ImageBasicInfo> blockSearchBySecond = 
                imageService.SearchByKeywords("second", 0, 1);

            List<ImageBasicInfo> listBlockFirst = new List<ImageBasicInfo>();
            listBlockFirst.Add(image1Info);
            Block<ImageBasicInfo> blockSearchByFirst =
                imageService.SearchByKeywords("first", 0, 1);

            List<ImageBasicInfo> listBlockFirstAndSecond = new List<ImageBasicInfo>();
            listBlockFirstAndSecond.Add(image1Info);
            Block<ImageBasicInfo> blockSearchByFirstAndSecondAndExistMoreItems =
                imageService.SearchByKeywords("first sec", 0, 1);

            for (int i = 0; i < listBlockSecond.Count; i++)
            {
                ImageBasicInfo imageInfoList = listBlockSecond.ElementAt(i);
                ImageBasicInfo imageInfoBlockList = blockSearchBySecond.items.ElementAt(i);
                Assert.AreEqual(imageInfoList.title, imageInfoBlockList.title);
                Assert.AreEqual(imageInfoList.userId, imageInfoBlockList.userId);
                Assert.AreEqual(imageInfoList.userName, imageInfoBlockList.userName);
            }
            Assert.IsFalse(blockSearchBySecond.existMoreItems);
            for (int i = 0; i < listBlockFirst.Count; i++)
            {
                ImageBasicInfo imageInfoList = listBlockFirst.ElementAt(i);
                ImageBasicInfo imageInfoBlockList = blockSearchByFirst.items.ElementAt(i);
                Assert.AreEqual(imageInfoList.title, imageInfoBlockList.title);
                Assert.AreEqual(imageInfoList.userId, imageInfoBlockList.userId);
                Assert.AreEqual(imageInfoList.userName, imageInfoBlockList.userName);
            }
            Assert.IsFalse(blockSearchBySecond.existMoreItems);
            for (int i = 0; i < listBlockFirstAndSecond.Count; i++)
            {
                ImageBasicInfo imageInfoList = listBlockFirstAndSecond.ElementAt(i);
                ImageBasicInfo imageInfoBlockList = blockSearchByFirstAndSecondAndExistMoreItems.items.ElementAt(i);
                Assert.AreEqual(imageInfoList.title, imageInfoBlockList.title);
                Assert.AreEqual(imageInfoList.userId, imageInfoBlockList.userId);
                Assert.AreEqual(imageInfoList.userName, imageInfoBlockList.userName);
            }
            Assert.IsTrue(blockSearchByFirstAndSecondAndExistMoreItems.existMoreItems);

            string imagePath = appSettings[ImagesPathKey];
            Assert.IsTrue(File.Exists(imagePath + "\\" + image2.path));
            TestContext.WriteLine(imagePath + "\\" + image2.path);

            // No puedes borrar porque has sacado de base de datos una entidad
            // que guarda relaciones con otras entidades
            long im1Id = image1.imgId, im2Id = image2.imgId;
            long im1UserId = image1.userId, im2UserId = image2.userId;
            imageService.DeleteImage(im1Id, im1UserId);
            imageService.DeleteImage(im2Id, im2UserId);

            Assert.IsFalse(File.Exists(imagePath + "\\" + image2.path));
             
        }
    }
}
