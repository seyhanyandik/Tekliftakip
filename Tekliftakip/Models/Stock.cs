using System.ComponentModel.DataAnnotations;

namespace Tekliftakip.Models
{
    public class Stock
    {
        [Key]

        public int StockId { get; set; }
        [Display(Name = "Ürün Stok Kodu")]
        [Required(ErrorMessage = "Bu Alan Boş Bırakılmaz")]
        public int StockCode { get; set; }
        [Display(Name = "Ürün Stok Miktarı")]
        public int StockAmount { get; set; }

        virtual public List<Product>? Products { get; set; }
    }
}
