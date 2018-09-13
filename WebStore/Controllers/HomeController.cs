using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using WebStore.Context;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(c => c.Category).ToListAsync();
            ViewBag.Home = "active";
            ViewBag.CategoryName = "";
            return View(products);
        }

        public async Task<IActionResult> InvoicePdf(int orderId)
        {
            if (orderId == 0)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(c => c.CartItems).FirstOrDefaultAsync(t => t.Id == orderId);
            var cart = _context.CartItems.Include(t => t.Order).Where(t => t.Order.Id == orderId).ToList();
            Invoice GetOne()
            {
                var invoice = new Invoice();
                invoice.FirstName = order.FirstName;
                invoice.LastName = order.LastName;
                invoice.Address = order.Address;
                invoice.City = order.City;
                invoice.Zip = order.Zip;
                invoice.State = order.State;
                invoice.Country = order.Country;
                invoice.PaymentDue = DateTime.Now.AddDays(30);
                invoice.InvoiceDate = DateTime.Now.Date;
                invoice.Email = order.Email;

                var items = new List<InvoiceItem>();
                var inv = new InvoiceItem();

                foreach (var item in cart)
                {
                    inv = new InvoiceItem
                    {
                        Quantity = item.Quantity,
                        Title = item.Title,
                        Author = item.Author,
                        Price = item.Price,
                    };
                    items.Add(inv);
                }
                invoice.Items = items;
                return invoice;
            }
            return new ViewAsPdf("Invoice", GetOne());
        }
    }
}
