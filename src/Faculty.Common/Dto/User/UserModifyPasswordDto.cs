namespace Faculty.Common.Dto.User
{
    /// <summary>
    /// Model User data transfer object for modify password.
    /// </summary>
    public class UserModifyPasswordDto
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
