using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class Group
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
        /// Foreign key for specialization entity.
        /// </summary>
        public int? SpecializationId { get; set; }

        /// <summary>
        /// Entity specialization.
        /// </summary>
        public virtual Specialization Specialization { get; set; }

        /// <summary>
        /// Faculties group.
        /// </summary>
        public virtual ICollection<Faculty> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Group.
        /// </summary>
        public Group()
        {
            Faculties = new HashSet<Faculty>();
        }
    }
}
