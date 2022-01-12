using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{

    public interface IUserService

    {
        [Inject]
        IUserDao UserDao { set; }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordIsEncrypted">if set to <c>true</c> [password is encrypted].</param>
        /// <returns cref="LoginResult">The login data retrieved.</returns>
        /// <exception cref="InstanceNotFoundException">
        /// When no user with the username given is found.</exception>
        /// <exception cref="IncorrectPasswordException">
        /// When the password used for login does not match the 
        /// stored in DB.</exception>
        [Transactional]
        LoginResult Login(String userName, String password, Boolean passwordIsEncrypted);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="userDetails">The user details.</param>
        /// <returns></returns>
        /// <exception cref="DuplicateInstanceException">
        /// When a user with the same username already exists.
        /// </exception>
        [Transactional]
        long RegisterUser(string userName, string password, UserProfileDetails userDetails);


        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newUserDetails">The new user details.</param>
        [Transactional]
        void UpdateUserProfileDetails(long userId, UserProfileDetails newUserDetails);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <exception cref="InstanceNotFoundException">
        /// When the user by the id is not found.</exception>
        /// <exception cref="IncorrectPasswordException">
        /// When the password stored in db and password
        /// passed from user are not equal.</exception>
        [Transactional]
        void ChangePassword(long userId, string oldPassword, string newPassword);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns cref="UserProfileDetails">THe Dto with user profile information.</returns>
        /// <exception cref="InstanceNotFoundException">
        /// When the user by the id is not found.</exception>
        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userId);

        /// <summary>
        /// Deletes the account.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="InstanceNotFoundException">
        /// When the user by the id is not found.</exception>
        [Transactional]
        void BorrarCuenta(long userId);

        /// <summary>
        /// Checks if an user exists by a given identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        bool UserExists(long userId);

        /// <summary>
        /// Checks if an user exists by a given user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        bool UserExists(string userName);

        /// <summary>
        /// Returns the id of a user given its userName.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The id of the tuser.</returns>
        long FindUserId(string userName);

        Block<UserInfo> FindFollowed(long userId, int startIndex, int count);

    }
}
