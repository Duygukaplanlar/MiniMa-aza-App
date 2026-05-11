using Microsoft.AspNetCore.Mvc;
using MiniMagaza.Models;
using MiniMagaza.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MiniMagaza.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartJson = HttpContext.Session.GetString("Sepetim");
            List<Product> cart = string.IsNullOrEmpty(cartJson)
                ? new List<Product>()
                : JsonConvert.DeserializeObject<List<Product>>(cartJson);

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                var cartJson = HttpContext.Session.GetString("Sepetim");
                List<Product> cart = string.IsNullOrEmpty(cartJson)
                    ? new List<Product>()
                    : JsonConvert.DeserializeObject<List<Product>>(cartJson);

                cart.Add(product);
                HttpContext.Session.SetString("Sepetim", JsonConvert.SerializeObject(cart));
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartJson = HttpContext.Session.GetString("Sepetim");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<List<Product>>(cartJson);
                var itemToRemove = cart.FirstOrDefault(p => p.Id == id);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.SetString("Sepetim", JsonConvert.SerializeObject(cart));
                }
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult CompleteOrder()
        {
            var cartJson = HttpContext.Session.GetString("Sepetim");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<List<Product>>(cartJson);

                foreach (var item in cart)
                {
                    var dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (dbProduct != null && dbProduct.Stock > 0)
                    {
                        dbProduct.Stock -= 1; 
                    }
                }

                _context.SaveChanges();
                HttpContext.Session.Remove("Sepetim");
            }

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
