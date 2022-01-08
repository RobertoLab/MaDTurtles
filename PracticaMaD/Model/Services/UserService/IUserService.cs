using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{

    public interface IUserService

    {
        [Inject]
        IUserDao UserDao { set; }
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        LoginResult Login(String userName, String password, Boolean passwordIsEncrypted);

        [Transactional]
        /// <exception cref="DuplicateInstanceException"/>
        long RegisterUser(string userName, string password, UserProfileDetails userDetails);


        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newUserDetails">The new user details.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserDetails(long userId, UserProfileDetails newUserDetails);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        void ChangePassword(long userId, string oldPassword, string newPassword);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>THe Dto with user profile information.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userId);

        /// <summary>
        /// Deletes the account.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void BorrarCuenta(long userId);
    }
}
