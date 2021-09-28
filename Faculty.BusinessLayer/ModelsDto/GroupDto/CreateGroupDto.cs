namespace Faculty.BusinessLayer.ModelsDto.GroupDto
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class CreateGroupDto
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
