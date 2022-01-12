using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class UserInfo
    {
        public long userId;

        public string userName;

        public UserInfo(long userId, string userName)
        {
            this.userId = userId;
            this.userName = userName;
        }
    }
}
