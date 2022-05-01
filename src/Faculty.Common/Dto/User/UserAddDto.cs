namespace Faculty.Common.Dto.User
{
    /// <summary>
    /// Model User data transfer object for add.
    /// </summary>
    public class UserAddDto : UserDto
    {
        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
    }
}
