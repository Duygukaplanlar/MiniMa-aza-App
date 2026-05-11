using Microsoft.AspNetCore.Mvc;
using MiniMagaza.Models;
using MiniMagaza.Data;
using System.Linq;

namespace MiniMagaza.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Ana Sayfa ve Kategori Filtreleme
        public IActionResult Index(int? categoryId)
        {
            // Veritabanından ürünleri çekiyoruz
            var urunlerQuery = _context.Products.AsQueryable();

            // Eğer bir kategori seçildiyse ona göre filtrele
            if (categoryId.HasValue)
            {
                urunlerQuery = urunlerQuery.Where(p => p.CategoryId == categoryId);
            }

            var model = urunlerQuery.ToList();
            return View(model);
        }

        // Ürün Detay Sayfası
        public IActionResult Details(int id)
        {
            // ID'ye göre ilgili ürünü bul
            var urun = _context.Products.FirstOrDefault(p => p.Id == id);

            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
