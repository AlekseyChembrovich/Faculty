using System.ComponentModel.DataAnnotations;

namespace Faculty.BusinessLayer.ModelsDto
{
    /// <summary>
    /// Entity Curator.
    /// </summary>
    public class CreateCuratorDto
    {
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
        [Phone]
        public string Phone { get; set; }
    }
}
