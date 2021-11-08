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
    }
}


