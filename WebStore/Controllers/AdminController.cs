using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebStore.Context;
using WebStore.Models;
using WebStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.Seed(HttpContext.RequestServices);
            return RedirectToAction("Index", "Admin");
        }
    }
}
