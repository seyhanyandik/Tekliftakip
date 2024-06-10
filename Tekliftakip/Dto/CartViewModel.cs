using Tekliftakip.Models;

namespace Tekliftakip.Dto
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }

        public decimal GrandTotal { get; set; }

    }
}
