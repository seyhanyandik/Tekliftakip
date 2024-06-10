using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace Tekliftakip.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Ürün Adı")]
        public string? ProductName { get; set; } = string.Empty;
        [Display(Name = "Ürün Kodu")]
        public int? ProductCode { get; set; }
        [Display(Name = "Ürün Fiyatı")]
        public int? ProductPrice { get; set; }
        [Display(Name = "Ürün Türü")]
        public string ProductType { get; set; } = string.Empty;
        [Display(Name = "Ürün Resmi")]
        public string? ProductPicture { get; set; }
        [Display(Name = "Ürün Stoğu")]
        public int StockId { get; set; }

        virtual public Stock? Stock { get; set; }

        [NotMapped]

        public IFormFile? ImageUpload { get; set; }

        virtual public BidForm? BidForm { get; set; }


        virtual public BidCart? BidCart { get; set; }
    }
}
