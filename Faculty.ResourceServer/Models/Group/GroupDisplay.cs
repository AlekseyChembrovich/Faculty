namespace Faculty.ResourceServer.Models.Group
{
    /// <summary>
    /// ViewModel Group.
    /// </summary>
    public class GroupDisplay
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
        /// Name specialization.
        /// </summary>
        public string SpecializationName { get; set; }
    }
}
