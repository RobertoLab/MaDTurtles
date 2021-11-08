using Es.Udc.DotNet.Photogram.Model;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Data.Entity;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.Photogram.Test;
namespace Es.Udc.DotNet.Photogram.Model.UserDao.Tests
{
    [TestClass]
    public class UserDaoTest
    {

        private static IKernel kernel;
        private TestContext testContextInstance;
        private User user;
        private static IUserDao userDao;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            user = new User();
            user.userName = "jsmith";
            user.password = "password";
            user.firstName = "John";
            user.lastName1 = "Smith";
            user.lastName2 = "Smith";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "US";

            userDao.Create(user);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            try
            {
                userDao.Remove(user.userId);
            }
            catch (Exception)
            {
            }
        }

        public void DAO_UpdateTest()
        {
            try
            {
                user.userName = "johansmith";
                user.password = "password";
                user.firstName = "Johan";
                user.lastName1 = "Smith";
                user.lastName2 = "Smith";
                user.email = "jsmith@acme.com";
                user.language = "en";
                user.country = "US";

                userDao.Update(user);

                User actual = userDao.Find(user.userId);

                Assert.AreEqual(user, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_RemoveTest()
        {
            try
            {
                userDao.Remove(user.userId);

                bool userExists = userDao.Exists(user.userId);

                Assert.IsFalse(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }



        [TestMethod()]
        public void DAO_CreateTest()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
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

                bool userExists = userDao.Exists(newUser.userId);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void DAO_AttachTest()
        {
            User user2 = userDao.Find(user.userId);
            userDao.Remove(user.userId);   // removes the user created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<User, Int64>)userDao).Context;

            // Check the user is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            userDao.Attach(user);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Unchanged);

        }

        [TestMethod()]
        public void DAO_FindByUserNameTest()
        {

            User user2 = userDao.FindByUserName(user.userName);

            Assert.AreEqual(user2.userName, user.userName);
        }

    }
}

