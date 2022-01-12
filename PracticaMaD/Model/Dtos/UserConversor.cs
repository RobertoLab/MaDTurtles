using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    class UserConversor
    {
        public static UserInfo ToUserInfo(User user)
        {
            return new UserInfo(user.userId, user.userName);
        }

        public static List<UserInfo> ToUserInfos(List<User> users)
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            users.ForEach(user => userInfos.Add(ToUserInfo(user)));
            return userInfos;
        }
    }
}
