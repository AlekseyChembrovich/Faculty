namespace Faculty.AuthenticationServer.Models.User
{
    /// <summary>
    /// Model User for modify password.
    /// </summary>
    public class UserModifyPassword
    {
        /// <summary>
        /// User unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User new password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
