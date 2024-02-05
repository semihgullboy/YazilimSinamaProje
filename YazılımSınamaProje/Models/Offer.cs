using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class Offer
    {
        [Key]

        public int offerID { get; set; }

        public int bidderID { get; set; }

        public int offerRecipientID { get; set; }

        public int bidAmount { get; set; }

        public int projectID { get; set; }

        public Project Project { get; set; }


    }
}
