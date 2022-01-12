using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Photogram.Model.UserDao
{
    public interface IUserDao:IGenericDao<User, Int64>
    {

        ///<summary>
        /// Finds and User by userName
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>The User</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        User FindByUserName(string userName);

        /// <summary>
        /// Updates the images liked by the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateImagesLiked(User user);


        /// <summary>
        /// Updates the users followed by the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateUserFollow(User user);
        /// <summary>
        /// User exists by given username.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if it exists, false otherwise.</returns>
        bool Exists(string userName);

        /// <summary>
        /// Finds the id of a user given its userName.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The id of the user.</returns>
        long FindUserId(string userName);

        /// <summary>
        /// Returns a list of users followed by the user specified as userId
        /// If none is found returns an empty list.
        /// </summary>
        /// <param name="userId">Id of the user that follows</param>
        /// <param name="startIndex">The point at which be start recovering users</param>
        /// <param name="count">The number of users we recover</param>
        /// <returns>The list of users followed retrieved from DB.</returns>
        List<User> FindFollowed(long userId, int startIndex, int count);
    }
}


