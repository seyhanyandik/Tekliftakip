using System.ComponentModel.DataAnnotations;

namespace Tekliftakip.Models
{
    public class BidForm
    {
        [Key]
        public int BidId { get; set; }

        public DateTime? BidDate { get; set; }

        public int CustomerId { get; set; }

        public int? Discount { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int Piece { get; set; }

        public decimal Price { get; set; }

        virtual public BidCart? BidCart { get; set; }
        virtual public List<Customer>? Customers { get; set; }
        virtual public List<Product>? Products { get; set; }

        

    }
}
