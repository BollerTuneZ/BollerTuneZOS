using System.Collections.Generic;
using OS.Data.Secruity;

namespace OS.Infrastructure.Secruity
{
    /// <summary>
    /// Manage the UserAccounts
    /// Jonas Ahlf 30.07.2015 13:59:59
    /// </summary>
    public interface IAccountManagement
    {
        /// <summary>
        /// Displays all Usernames
        /// </summary>
        /// <returns></returns>
        IList<string> Usernames();
        /// <summary>
        /// Try to login user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>null=fail</returns>
        AuthToken Login(string username, string password);

        /// <summary>
        /// Loggs given user out
        /// </summary>
        void Logout(AuthToken user);

        /// <summary>
        /// Creates, if permitted, a new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="loggedInUser"></param>
        /// <returns></returns>
        bool CreateUser(UserMask newUser, AuthToken loggedInUser);

    }

    public class UserMask
    {
        #region Personal Data (Optional)
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        #endregion
        public string Login { get; set; }
        public string Password { get; set; }
    }
}