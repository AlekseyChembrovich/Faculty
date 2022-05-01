namespace Faculty.Common.Dto.LoginRegister
{
    /// <summary>
    /// Model User for auth operation.
    /// </summary>
    public class AuthUserDto
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
