using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{
    public class UserService: IUserService
    { 

        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <exception cref="InstanceNotFoundException"/>
            /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(string userName, string password, bool passwordIsEncrypted)
        {
            User user = UserDao.FindByUserName(userName);

            String pass = user.password;

            if (!pass.Equals(password))
            {
                throw new IncorrectPasswordException(userName);
            }

            return new LoginResult(user.userName, user.password, user.userId);

        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string userName, string password, string firstName, string lastName, string lastName2, string email, string country, string language)
        {
            User user = UserDao.FindByUserName(userName);
            if (user == null)
            {

                user.userName = userName;
                user.password = password;
                user.firstName = firstName;
                user.lastName1 = lastName;
                user.lastName2 = lastName2;
                user.email = email;
                user.language = language;
                user.country = country;

                UserDao.Create(user);
                return user.userId;
            }

            else
            {
                throw new DuplicateInstanceException(null, userName);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void BorrarCuenta(long userId)
        {
            User user = UserDao.Find(userId);

            UserDao.Remove(userId);
        }
    }
}
