using System.ComponentModel.DataAnnotations;

namespace GasHub.Models
{
    public class Gashub
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string CompanyID { get; set; }

        // Mailing Address
        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public string MailCity { get; set; }
        public string MailState { get; set; }
        public string MailZip { get; set; }
        public string MailCountry { get; set; }

        // Shipping Address
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public string ShipCountry { get; set; }

        // Accounts
        public string GLAccount { get; set; }
        public string SubAccount { get; set; }

        // Contact Info
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyCare { get; set; }
    }
}
