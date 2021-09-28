namespace Faculty.BusinessLayer.ModelsDto.CuratorDto
{
    /// <summary>
    /// Entity Curator.
    /// </summary>
    public class DisplayCuratorDto
    {
        /// <summary>
        /// Unique identificator curator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Surname curator.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Name curator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Doublename curator.
        /// </summary>
        public string Doublename { get; set; }

        /// <summary>
        /// Phone curator.
        /// </summary>
        public string Phone { get; set; }
    }
}
