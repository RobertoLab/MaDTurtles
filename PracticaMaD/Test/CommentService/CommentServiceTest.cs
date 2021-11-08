using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.IO;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.Photogram.Model.CommentService;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;


namespace Es.Udc.DotNet.Photogram.Model.CommentService.Test
{
    /// <summary>
    /// Tests for proper functionality on ImageService.
    /// </summary>
    [TestClass]
    public class CommentServiceTest
    {
        private static IKernel kernel;
        private static ICommentService commentService;
        private static long testUserId = 1;
        private static long testImgId = 1;

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
            commentService = kernel.Get<ICommentService>();
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
        public void CommentServiceMethodsTest()
        {
            Comment comment = commentService.PostComment("Comentario 1 del Test", testUserId, testImgId);

            Assert.Equals("Comentario 1 del Test", comment.comment);

            Comment commentEdited = commentService.EditComment(comment.commentId, "Texto editado");

            Assert.AreNotEqual(comment, commentEdited);

            Comment comment2 = commentService.PostComment("Comentario 2 del Test", testUserId, testImgId);

            commentService.DeleteComment(comment.commentId);

            List<Comment> comments = commentService.GetImageComments(testImgId);

            Assert.Equals(1, comments.Count);
               
        }

    }
}

