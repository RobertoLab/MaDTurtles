using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Photogram.Model.UserService
{
    [Serializable()]
    public class LoginResult
    {

        public LoginResult (String userName, String password, long userId)
        {
            this.userName = userName;
            this.password = password;
            this.userId = userId;
        }

        public String userName { get; private set; }

        public String password { get; private set; }

        public long userId { get; private set; }


    }
}
