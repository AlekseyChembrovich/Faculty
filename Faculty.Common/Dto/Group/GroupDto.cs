namespace Faculty.Common.Dto.Group
{
    /// <summary>
    /// Dto Group.
    /// </summary>
    public class GroupDto
    {
        /// <summary>
        /// Unique group id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Foreign key for specialization entity.
        /// </summary>
        public int SpecializationId { get; set; }
    }
}
