namespace Faculty.AuthenticationServer.Models.LoginRegister
{
    /// <summary>
    /// Model User for login.
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
    }
}
