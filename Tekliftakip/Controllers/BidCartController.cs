using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Tekliftakip.Data;
using Tekliftakip.Models;


namespace Tekliftakip.Controllers
{
    public class BidCartController : Controller
    {
        private readonly ApplicationDbContext _context;
              

        public BidCartController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }
        //Teklifleri Karşılaştırma Admin Panelinden
        public IActionResult BidCompare() {
            ViewBag.ProductList= new SelectList(_context.Products, "ProductId", "ProductName");
            var sorgu=_context.BidCarts.ToList();
            return View(sorgu);
        }
        //Teklif Filtreleme
        [HttpGet]
        public IActionResult BidCart(string id)
        {
            if (int.TryParse(id, out int productId))
            {
                var sorgu = _context.BidCarts.Where(x => x.ProductId == productId).ToList();
                return Json(sorgu); // View yerine Json olarak döndürüyoruz.
            }

            return Json(new List<BidCart>()); // Eğer id parse edilemiyorsa boş bir liste döndürüyoruz.
        }

        //Teklifleri Ajax ile Ekleme Müşteri panelinden
        [HttpPost]
        public IActionResult BidCartAdd(BidCart model)
        {
           
            try
            {
                var bidCart = new BidCart
                {
                    Image = model.Image,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Piece = model.Piece,
                    BidPrice = model.BidPrice,
                    CustomerId = model.CustomerId,
                    ProductId = model.ProductId
                };


                _context.BidCarts.Add(bidCart);
                _context.SaveChanges();
                return Json(new { success = true, message = "Teklif başarıyla gönderildi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Teklif gönderilirken bir hata oluştu: {ex.Message}" });
            }


            
        }
    }
}
