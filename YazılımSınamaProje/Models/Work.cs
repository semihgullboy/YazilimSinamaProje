using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class Work
    {
        [Key]
        public int WorkID { get; set; }

        public int WorkBusinessID { get; set; }

        public int EmployerID { get; set; }

        public int ProjectID { get; set; }

        public int NewBudget { get; set; }

        public string? Explanation { get; set; }

        public Project Project { get; set; }
    }
}
