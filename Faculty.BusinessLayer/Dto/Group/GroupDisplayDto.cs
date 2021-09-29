namespace Faculty.BusinessLayer.Dto.Group
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class GroupDisplayDto
    {
        /// <summary>
        /// Unique identificator curator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Foreign key for specialization entity.
        /// </summary>
        public string SpecializationName { get; set; }
    }
}
