using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Models;
using WebStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet]
        public IActionResult Orders()
        {
            ViewBag.OrderItems = _context.CartItems.Include(t => t.Order).Where(i => i.Order.UserName == User.Identity.Name).ToList();
            var orders = _context.Orders.Include(s => s.Status).Where(t => t.UserName == User.Identity.Name).ToList();
            ViewBag.MyAccountOrders = "active";
            return View(orders);
        }

        [HttpGet]
        public IActionResult Details(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }
            else
            {
                var cartItems = _context.CartItems.Include(t => t.Order).Where(i => i.Order.Id == orderId).ToList();
                ViewBag.MyAccountOrderHistory = "active";
                return View(cartItems);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            ViewBag.UserReview = _context.Reviews.Include(p => p.Product).Where(r => r.UserName == userName).ToList();
            ViewBag.MyAccountProfile = "active";
            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.MyAccountPassword = "active";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var result = await _userManager.ChangePasswordAsync(user,
                    viewModel.CurrentPassword, viewModel.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("LogIn", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
            }
            return View(viewModel);
        }
    }
}
