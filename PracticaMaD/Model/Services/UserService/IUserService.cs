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

        [Transactional]
        void BorrarCuenta(long userId);
    }
}
