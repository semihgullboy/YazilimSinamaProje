using System.ComponentModel.DataAnnotations;

namespace YazılımSınamaProje.Models
{
    public class Offer
    {
        [Key]
        public int OfferID { get; set; }

        public int BidderID { get; set; }

        public int OfferRecipientID { get; set; }

        public int BidAmount { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }
    }
}
