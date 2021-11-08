using System;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.MiniPortal.Model.UserService.Exceptions
{

    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class IncorrectPasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IncorrectPasswordException"/> class.
        /// </summary>
        /// <param name="userName"><c>userName</c> that causes the error.</param>
        public IncorrectPasswordException(String userName)
            : base("Incorrect password exception => userName = " + userName)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public String userName { get; private set; }
    }
}
