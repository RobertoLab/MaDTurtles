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

        public LoginResult (String userName, String password, long userId,
            string country, string language)
        {
            this.userName = userName;
            this.password = password;
            this.userId = userId;
            this.country = country;
            this.language = language;
        }

        public String userName { get; private set; }

        public String password { get; private set; }

        public long userId { get; private set; }

        public string country { get; private set; }

        public string language { get; private set; }


    }
}
