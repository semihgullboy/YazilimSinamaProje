using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class Work
    {
        [Key]
        public int workID { get; set; }

        public int workBusinessID { get; set; }

        public int employerID { get; set; }

        public int projectID { get; set; }

        public int newBudget { get; set; }

        public string? explanation { get; set; }

        public Project project { get; set; }
    }
}
