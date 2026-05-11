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

        
        public IActionResult Index(int? categoryId)
        {
            
            var urunlerQuery = _context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                urunlerQuery = urunlerQuery.Where(p => p.CategoryId == categoryId);
            }

            var model = urunlerQuery.ToList();
            return View(model);
        }

   
        public IActionResult Details(int id)
        {
            
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
