using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Helpers;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            if (cart == null)
            {
                return View("Empty");
            }
            ViewBag.CartTotal = cart.Sum(item => item.Product.Price * item.Quantity);
            ViewBag.ShoppingCart = "active";
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") == null)
            {
                var cart = new List<CartItem>();
                var productToBuy = _context.Products.FirstOrDefault(p => p.Id == id);
                cart.Add(new CartItem { Product = productToBuy, Quantity = 1 });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

                var index = IsExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    var productAddToCart = _context.Products.SingleOrDefault(p => p.Id == id);
                    cart.Add(new CartItem { Product = productAddToCart, Quantity = 1 });
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            var index = IsExist(id);
            if (index == -1)
            {
                return View("Index");
            }
            else
            {
                cart.RemoveAt(index);
                HttpContext.Session.SetObjectAsJson("cart", cart);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult RemoveAll()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            if (cart == null)
            {
                return View("Index");
            }
            else
            {
                cart.Clear();
                HttpContext.Session.SetObjectAsJson("cart", cart);
                return RedirectToAction("Index");
            }
        }

        private int IsExist(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}