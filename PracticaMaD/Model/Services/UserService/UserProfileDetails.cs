using System;


namespace Es.Udc.DotNet.Photogram.Model.UserService
{

    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class UserProfileDetails
    {
        #region Properties Region

        public String firstName { get; private set; }

        public String lastName { get; private set; }

        public String email { get; private set; }

        public string language { get; private set; }

        public string country { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        public UserProfileDetails(String firstName, String lastName,
            String email, String language, String country)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.language = language;
            this.country = country;
        }

        public override bool Equals(object obj)
        {

            UserProfileDetails target = (UserProfileDetails)obj;

            return (this.firstName == target.firstName)
                  && (this.lastName == target.lastName)
                  && (this.email == target.email)
                  && (this.language == target.language)
                  && (this.country == target.country);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.firstName.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + firstName + " | " +
                "lastName = " + lastName + " | " +
                "email = " + email + " | " +
                "language = " + language + " | " +
                "country = " + country + " ]";


            return strUserProfileDetails;
        }
    }
}
