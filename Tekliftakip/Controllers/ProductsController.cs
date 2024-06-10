using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tekliftakip.Data;
using Tekliftakip.Models;

namespace Tekliftakip.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Stock);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId");
            ViewBag.kateliste = new SelectList(_context.Stocks, "StockId", "StockCode");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductCode,ProductPrice,ProductType,ProductPicture,StockId")] Product product,IFormFile ImageUpload)
        {
            if (ImageUpload != null)
            {
                var uzanti = Path.GetExtension(ImageUpload.FileName);
                //bocek.png  .png domates.jpg  .jpg
                string yeniisim = Guid.NewGuid().ToString() + uzanti;

                string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + yeniisim);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    ImageUpload.CopyToAsync(stream);
                }
                product.ProductPicture = yeniisim;
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId", product.StockId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            //ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId", product.StockId);
            ViewBag.kateliste = new SelectList(_context.Stocks, "StockId", "StockCode");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductCode,ProductPrice,ProductType,ProductPicture,StockId")] Product product, IFormFile ImageUpload)
        {
            if (ImageUpload != null)
            {
                var uzanti = Path.GetExtension(ImageUpload.FileName);
                //bocek.png  .png domates.jpg  .jpg
                string yeniisim = Guid.NewGuid().ToString() + uzanti;

                string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + yeniisim);
                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    ImageUpload.CopyToAsync(stream);
                }
                product.ProductPicture = yeniisim;
            }

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId", product.StockId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            //Dosya Silme
            string yol = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + product.ProductPicture);
            FileInfo yolFile = new FileInfo(yol);
            if (yolFile.Exists)
            {
                System.IO.File.Delete(yolFile.FullName);
                yolFile.Delete();
            }
            //Dosya Silme
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
