using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Country { get; set; }

        public int? Money { get; set; }
    }
}
