using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMagaza.Data;
using MiniMagaza.Models;

namespace MiniMagaza.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Ürün Listeleme (Anasayfa)
        public async Task<IActionResult> Index(int? categoryId)
        {
            // Veritabanındaki tüm ürünleri getir ama Kategorilerini de içine dahil et (Include)
            var urunlerQuery = _context.Products.Include(p => p.Category).AsQueryable();

            // Eğer bir kategoriye tıklandıysa (categoryId dolu geldiyse) listeyi filtrele
            if (categoryId.HasValue)
            {
                urunlerQuery = urunlerQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            return View(await urunlerQuery.ToListAsync());
        }

        // 2. Ürün Detay Sayfası
        public async Task<IActionResult> Details(int id)
        {
            // ID'ye göre ürünü bulur, yoksa 404 hatası verir
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}