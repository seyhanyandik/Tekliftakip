using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace Tekliftakip.Models
{
    public class BidCart
    {
        [Key]
        public int CartId { get; set; }

        public int BidId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Piece { get; set; }

        public decimal Price { get; set; }
        public decimal BidPrice { get; set; }

        public string Image { get; set; }

       
        virtual public List<BidForm>? BidForms { get; set; }

        virtual public List<Customer>? Customers { get; set; }

        virtual public List<Product>? Products { get; set; }


    }
}