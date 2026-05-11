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

        
        public async Task<IActionResult> Index(int? categoryId)
        {
           
            var urunlerQuery = _context.Products.Include(p => p.Category).AsQueryable();

           
            if (categoryId.HasValue)
            {
                urunlerQuery = urunlerQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            return View(await urunlerQuery.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int id)
        {
            
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
