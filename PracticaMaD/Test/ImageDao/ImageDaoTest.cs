using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Test;

namespace Es.Udc.DotNet.Photogram.Model.ImageDao.Tests
{
    [TestClass]
    public class ImageDaoTest
    {
        public ImageDaoTest() { }

        private static IKernel kernel;
        private static IUserDao userDao;
        private static IImageDao imageDao;
        private static ICategoryDao categoryDao;
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
            userDao = kernel.Get<IUserDao>();
            imageDao = kernel.Get<IImageDao>(); 
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

        /// <summary>
        /// Test create images, find image by id, find images by keywords.
        /// </summary>
        [TestMethod]
        public void StoreImages()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imageTestPath = appSettings[ImagesTestPathKey];
            // Get image to store as bytes
            byte[] imageAsByte = readStoredImage("bmx");

            System.DateTime imgUploadDate = System.DateTime.Now;

            // Create the image
            Image image = new Image();
            image.title = "bmx";
            image.description = "first test image";
            image.uploadDate = imgUploadDate;
            image.categoryId = testCatId;
            image.path = null;
            image.userId = testUserId;
            image.img = imageAsByte;

            imageDao.Create(image);

            // Create a second image
            Image image2 = new Image();
            image2.title = "bmx";
            image2.description = "second test image";
            image2.uploadDate = imgUploadDate;
            image2.categoryId = testCatId;
            image2.path = null;
            image2.userId = testUserId;
            image.img = imageAsByte;

            imageDao.Create(image2);

            Image imgStored = imageDao.Find(image.imgId);

            // Not Equal
            List<Image> imagesSecond = imageDao.FindByKeywords("second", 0, 10);
            List<Image> imagesFirst = imageDao.FindByKeywords("first", 0, 10);
            // Equal
            List<Image> imagesTest = imageDao.FindByKeywords("test", 0, 10);
            List<Image> imagesTesT = imageDao.FindByKeywords("tesT", 0, 10);
            List<Image> imagesTes = imageDao.FindByKeywords("tes", 0, 10);
            List<Image> imagesBmx = imageDao.FindByKeywords("bmx", 0, 10);
            List<Image> imagesMx = imageDao.FindByKeywords("mx", 0, 10);
            List<Image> imagesByNonExistentCat = imageDao.FindByKeywords("bmx", 999, 0, 10);
            List<Image> imagesByTestCat = imageDao.FindByKeywords("bmx", 1, 0, 10);
            List<Image> imagesByTest2Cat = imageDao.FindByKeywords("bmx", 2, 0, 10);


            CollectionAssert.AreNotEqual(imagesFirst, imagesSecond);
            CollectionAssert.AreEqual(imagesBmx, imagesMx);
            CollectionAssert.AreEqual(imagesTest, imagesTesT);
            CollectionAssert.AreEqual(imagesTest, imagesTes);
            CollectionAssert.AreEqual(imagesBmx, imagesByTestCat);
            Assert.AreEqual(0, imagesByTest2Cat.Count);
            Assert.AreEqual(0, imagesByNonExistentCat.Count);
            Assert.AreEqual(image, imgStored);

            imageDao.Remove(image.imgId);
            imageDao.Remove(image2.imgId);
        }
    }
}
