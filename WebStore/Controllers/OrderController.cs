using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Helpers;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        protected UserManager<AppUser> UserManager { get; }

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            if (cart == null)
            {
                return NotFound();
            }
            ViewBag.Cart = cart;
            ViewBag.CartItems = cart.Sum(item => item.Quantity);
            ViewBag.CartTotal = cart.Sum(item => item.Product.Price * item.Quantity);

            return View();
        }

        [HttpPost]
        public IActionResult Checkout([Bind("Id,FirstName,LastName,Email,Address,City,State,Zip,Country")] Order order)
        {
            if (order != null)
            {
                if (ModelState.IsValid)
                {
                    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

                    foreach (var cartItem in cart)
                    {
                        _context.CartItems.Add(new CartItem
                        {
                            Quantity = cartItem.Quantity,
                            Order = order,
                            Title = cartItem.Product.Title,
                            Author = cartItem.Product.Author,
                            Price = cartItem.Product.Price,
                            ImagePath = cartItem.Product.ImagePath,
                        });
                    }

                    order.OrderTotal = cart.Sum(item => item.Product.Price * item.Quantity);
                    order.OrderDate = DateTime.Now;
                    order.UserName = UserManager.GetUserName(User);
                    order.OrderStatusId = 1;

                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    return RedirectToAction("Confirmation");
                }
                else
                {
                    return View(order);
                }

            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            for (var i = 0; i < cart.Count; i++)
            {
                cart.RemoveAt(i);
            }

            HttpContext.Session.SetObjectAsJson("cart", cart);

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var orders = _context.Orders.Include(s => s.Status).ToList();
            return View(orders);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
            var cartItems = _context.CartItems.Include(t => t.Order).Where(i => i.Order.Id == id).ToList();
                return View(cartItems);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Status(int? orderId)
        {
            var order = _context.Orders.FirstOrDefault(t => t.Id == orderId);
            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Status([Bind("Id,OrderStatusId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Payment(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }
            else
            {
                var order = _context.Orders.FirstOrDefault(t => t.Id == orderId);
                return View(order);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult PaymentConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var order = _context.Orders.Include(s => s.Status).FirstOrDefault(t => t.Id == id);
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    order.OrderStatusId = 2;
                    _context.SaveChanges();
                }
                return RedirectToAction("Orders", "User");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Shipped(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var order = await _context.Orders.Include(s => s.Status).FirstOrDefaultAsync(t => t.Id == id);
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    var statusId = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.Name == "Shipped");
                    order.OrderStatusId = statusId.Id;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Order");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Completed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var order =  await _context.Orders.Include(s => s.Status).FirstOrDefaultAsync(t => t.Id == id);
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    var statusId = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.Name == "Completed");
                    order.OrderStatusId = statusId.Id;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Order");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Cancelled(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var order = await _context.Orders.Include(s => s.Status).FirstOrDefaultAsync(t => t.Id == id);
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    var statusId = await _context.OrderStatuses.FirstOrDefaultAsync(s => s.Name == "Cancelled");
                    order.OrderStatusId = statusId.Id;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Order");
            }
        }
    }
}
