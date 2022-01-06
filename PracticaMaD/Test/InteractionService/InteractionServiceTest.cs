using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Test;

namespace Es.Udc.DotNet.Photogram.Model.InteractionService.Test
{
    [TestClass]
    public class InteractionServiceTest
    {
        private static IKernel kernel;
        private static IInteractionService interactionService;
        private static IUserDao userDao;
        private static IImageDao imageDao;
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
            interactionService = kernel.Get<IInteractionService>();
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
        public void CommentMethodsTest()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string imageTestPath = appSettings[ImagesTestPathKey];
            // Get image to store as bytes
            User user = signUpUser("test");
            Image image = uploadImage(user.userId);

            Comment comment = interactionService.PostComment("Comentario 1 del Test", user.userId, image.imgId);
            string baseComment = comment.comment;

            Assert.AreEqual("Comentario 1 del Test", comment.comment);

            Comment commentEdited = interactionService.EditComment(comment.commentId, "Texto editado");

            Assert.AreNotEqual(baseComment, commentEdited.comment);

            Comment comment2 = interactionService.PostComment("Comentario 2 del Test", user.userId, image.imgId);

            interactionService.DeleteComment(comment.commentId);

            Block<CommentInfo> comments = interactionService.GetImageComments(image.imgId, 0, 10);

            Assert.AreEqual(false, comments.existMoreItems);
            Assert.AreEqual(1, comments.items.Count);
        }

        [TestMethod]
        public void LikeMethodsTest()
        {
            User user1 = signUpUser("test1");
            User user2 = signUpUser("test2");

            Image image1 = uploadImage(user1.userId);

            Assert.AreEqual(0, interactionService.GetImageLikes(image1.imgId));

            interactionService.LikeImage(user2.userId, image1.imgId);
            Assert.AreEqual(1, interactionService.GetImageLikes(image1.imgId));

            interactionService.LikeImage(user1.userId, image1.imgId);
            Assert.AreEqual(2, interactionService.GetImageLikes(image1.imgId));

            interactionService.Unlike(user2.userId, image1.imgId);
            Assert.AreEqual(1, interactionService.GetImageLikes(image1.imgId));
        }
    }
}
