using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public int ProjectBudget { get; set; }

        public string ProjectTime { get; set; }

        public string ProjectLanguage { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }
    }
}
