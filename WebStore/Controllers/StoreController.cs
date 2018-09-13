using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Models;
using WebStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly AppDbContext _context;

        public StoreController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Store/Index/5
        public async Task<IActionResult> Index(int? category)
        {
            ViewBag.Products = await _context.Products.Where(p => category == null || p.Category.Id == category).ToListAsync();
            ViewBag.Home = "active";
            var categories =  await _context.Categories.ToListAsync();

            if (category == null)
            {
                ViewBag.CategoryName = "All products";
            }
            else
            {
                ViewBag.CategoryId = category;
                var getCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category);
                ViewBag.CategoryName = getCategory.Name;
            }
            return View(categories);
        }

        // GET: /Store/Search
        public IActionResult Search(string search)
        {
            if (search == null)
            {
                return NotFound();
            }
            var products = _context.Products.Include(c => c.Category).ToList();
            ViewBag.Store = "active";
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                var result = products.Where(s => s.Title.ToLower().Contains(search.ToLower())).ToList();
                var author = products.Where(s => s.Author.ToLower().Contains(search.ToLower())).ToList();
                if (result.Count != 0)
                    return View(result);
                if (author.Count != 0)
                    return View(author);
            }
            return NotFound();
        }

        //
        // GET: /Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.ProductId = id.Value;
            ViewBag.Store = "active";
            var reviews = _context.Reviews.Where(r => r.ProductId.Equals(id.Value)).ToList();
            ViewBag.Reviews = reviews;

            var ratings = _context.Reviews.Where(r => r.ProductId.Equals(id.Value)).ToList();
            if (ratings.Count > 0)
            {
                var ratingSum = ratings.Sum(s => s.Rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count;
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.ratingCount = 0;
            }
            return View(product);
        }
    }
}
