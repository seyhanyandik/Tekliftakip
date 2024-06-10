using System.ComponentModel.DataAnnotations;

namespace Tekliftakip.Models
{
    public class CurrentAccount
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name = "Müşteri")]

        public int? CustomerId { get; set; }
        [Display(Name = "Hesap Kodu")]

        public int? AccountCode { get; set; }
        [Display(Name = "Hesap Adı")]

        public string? AccountName { get; set; }
        [Display(Name = "Vergi Numarası")]

        public int? TaxNumber { get; set; }

        virtual public List<Customer>? Customers { get; set; }
    }
}
