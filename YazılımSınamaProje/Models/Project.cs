using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class Project
    {
        [Key]

        public int projectID { get; set; }

        public string projectTitle { get; set; }

        public string projectDescription { get; set; }

        public int projectBudget { get; set; }

        public string projectTime { get; set; }

        public string projectLanguage { get; set; }

        public int userId { get; set; }

        public User User { get; set; }
    }
}
