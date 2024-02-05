using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class User
    {
        [Key]

        public int userID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string country { get; set; }

        public int? money { get; set; }
    }
}
