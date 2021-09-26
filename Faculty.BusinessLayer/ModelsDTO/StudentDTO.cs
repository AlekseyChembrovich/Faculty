namespace Faculty.BusinessLayer.ModelsDto
{
    /// <summary>
    /// Entity Student.
    /// </summary>
    public class StudentDto
    {
        /// <summary>
        /// Unique identificator curator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Surname student.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Name student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Doublename student.
        /// </summary>
        public string Doublename { get; set; }
    }
}
