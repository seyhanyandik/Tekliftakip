namespace Tekliftakip.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Piece { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public decimal Total
        {
            get { return Piece * Price; }
        }
        public CartItem() { }
        public CartItem(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Piece = 1;
            Price = Convert.ToDecimal(product.ProductPrice);
            Image = product.ProductPicture;
        }
    }
}
