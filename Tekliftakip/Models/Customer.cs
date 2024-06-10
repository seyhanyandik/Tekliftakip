using System.ComponentModel.DataAnnotations;

namespace Tekliftakip.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Müşteri Adı Soyadı")]

        public string? CustomerNameSurname { get; set; }
        [Display(Name = "Müşteri E-Mail Adresi")]

        public string? CustomerEMail { get; set; } = string.Empty;
        [Display(Name = "Müşteri Telefon No")]

        public string? CustomerPhone { get; set; }
        [Display(Name = "Müşteri Adresi")]

        public string? CustomerAddress { get; set; }

        virtual public CurrentAccount? CurrentAccount { get; set; }

        virtual public BidForm? BidForm { get; set; }

        virtual public BidCart? BidCart { get; set; }

    }
}
