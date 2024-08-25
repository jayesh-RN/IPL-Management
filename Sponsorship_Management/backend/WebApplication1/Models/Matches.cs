using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SponsorShipManagement.Models
{
    public class Matches
    {
        public int MatchID { get; set; }
        [Required]
        public string MatchName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime MatchDate { get; set; }
        [Required]
        public string Location { get; set; }

        public int amt_paid { get; set; }
    }

    public class q2
    {
        public int sponsorID { get; set; }
        public string industrytype { get; set; }
        public DateTime latest_pay { get; set; }
        public int numberofpay { get; set; }

    }

    public class q4
    {
        public string sponsorname { get; set; }
        public double numberOfspons { get; set; }
    }

    public class q1
    {
        public int contractid { get; set; }
        public DateTime paymentdate { get; set; }

        public decimal amountpaid { get; set; }
        public string paymentstatus { get; set; }
    }

}
