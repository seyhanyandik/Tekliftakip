using Microsoft.AspNetCore.Mvc;
using Tekliftakip.Data;
using Tekliftakip.Dto;
using Tekliftakip.Models;
using Tekliftakip.Oturum;

namespace Tekliftakip.Component
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel cartvm = new()
            {
                CartItems = items,
                GrandTotal = items.Sum(x => x.Piece * x.Price)
            };
            return View(cartvm);
        }
        public async Task<IActionResult> Add(int id)
        {
            Product product = _context.Products.Find(id);
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem bidId = items.FirstOrDefault(x => x.ProductId == id);
            if (bidId == null)
            {
                items.Add(new CartItem(product));
            }
            else
            {
                bidId.Piece += 1;
            }
            HttpContext.Session.SetJson("Cart", items);
            TempData["Mesaj"] = "Ürün Sepete Eklenmiştir";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem bidId = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if (bidId.Piece > 1)
            {
                bidId.Piece -= 1;
            }
            else
            {
                cart.RemoveAll(c => c.ProductId == id);
            }
            if (cart.Count > 0)
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["Mesaj"] = "Ürün Sepetten Silindi";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(c => c.ProductId == id);
            if (cart.Count > 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["Mesaj"] = "Ürün Sepeti Silindi";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
        public ActionResult MyCartList()
        {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel cartvm = new()
            {
                CartItems = items,
                GrandTotal = items.Sum(x => x.Piece * x.Price)
            };
            return View(cartvm);
        }
    }
}
