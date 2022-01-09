﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data.Entity;

namespace Es.Udc.DotNet.Photogram.Model.UserDao
{
    public class UserDaoEntityFramework : GenericDaoEntityFramework<User, Int64>, IUserDao
    {
 

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoEntityFramework()
        {
        }



        ///<summary>
        /// Finds and User by userName
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>The User</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public User FindByUserName(string userName)
        {
            User user = null;


            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.userName == userName
                 select u);

            user = result.FirstOrDefault();

            return user;

        }

        public void UpdateImagesLiked(User user)
        {
            DbSet<User> users = Context.Set<User>();

            Update(user);
        }

        public bool Exists(string userName)
        {
            DbSet<User> users = Context.Set<User>();

            var result =
                (from u in users
                 where u.userName == userName
                 select u);

            return result.Any();
        }
    }
   
}