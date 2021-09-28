namespace Faculty.BusinessLayer.ModelsDto.GroupDto
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class DisplayGroupDto
    {
        /// <summary>
        /// Unique identificator group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Entity specialization.
        /// </summary>
        public string SpecializationName { get; set; }
    }
}
