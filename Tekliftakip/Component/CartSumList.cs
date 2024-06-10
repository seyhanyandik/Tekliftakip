using Microsoft.AspNetCore.Mvc;
using Tekliftakip.Data;
using Tekliftakip.Dto;
using Tekliftakip.Models;
using Tekliftakip.Oturum;

namespace Tekliftakip.Component
{
    public class CartSumList:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartSumList(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
           List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
           
            CartViewModel cartVm = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Piece * x.Price)
            };
            return View(cartVm);
        }
    }
}
