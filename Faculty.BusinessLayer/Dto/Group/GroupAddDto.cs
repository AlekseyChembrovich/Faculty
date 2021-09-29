namespace Faculty.BusinessLayer.Dto.Group
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class GroupAddDto
    {
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
