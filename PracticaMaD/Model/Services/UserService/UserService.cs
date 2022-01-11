using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.Photogram.Model.UserService.Util;

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

            return new LoginResult(user.userName, user.password, user.userId,
                user.country, user.language);

        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string userName, string password, UserProfileDetails userDetails)
        {
            User user = new User();

            if (UserDao.FindByUserName(userName) == null)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(password);

                user.userName = userName;
                user.password = encryptedPassword;
                user.firstName = userDetails.firstName;
                user.lastName1 = userDetails.lastName;
                user.email = userDetails.email;
                user.language = userDetails.language;
                user.country = userDetails.country;

                UserDao.Create(user);
                return user.userId;
            }

            else
            {
                throw new DuplicateInstanceException(null, userName);
            }
        }

        public void UpdateUserProfileDetails(long userId, UserProfileDetails newUserDetails)
        {
            User user = UserDao.Find(userId);

            user.firstName = newUserDetails.firstName;
            user.lastName1 = newUserDetails.lastName;
            user.email = newUserDetails.email;
            user.country = newUserDetails.country;
            user.language = newUserDetails.language;

            UserDao.Update(user);
        }

        public void ChangePassword(long userId, string oldPassword, string newPassword)
        {
            User user = UserDao.Find(userId);

            string storedPassword = user.password;
            if (!PasswordEncrypter.IsClearPasswordCorrect(oldPassword, storedPassword))
            {
                throw new IncorrectPasswordException(user.userName);
            }

            user.password = PasswordEncrypter.Crypt(newPassword);
            UserDao.Update(user);
        }

        public UserProfileDetails FindUserProfileDetails(long userId)
        {
            User user = UserDao.Find(userId);

            UserProfileDetails userDetails = new UserProfileDetails(user.userName,
                user.firstName, user.lastName1, user.email, user.language,
                user.country);

            return userDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void BorrarCuenta(long userId)
        {
            User user = UserDao.Find(userId);

            UserDao.Remove(userId);
        }

        [Transactional]
        public bool UserExists(long userId)
        {
            return UserDao.Exists(userId);
        }

        [Transactional]
        public bool UserExists(string userName)
        {
            return UserDao.Exists(userName);
        }
    }
}
