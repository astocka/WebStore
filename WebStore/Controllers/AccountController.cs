using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AccountController : Controller
    {
        protected UserManager<AppUser> UserManager { get; }
        protected SignInManager<AppUser> SignInManager { get; }
        protected RoleManager<IdentityRole<int>> RoleManager;
        protected AppDbContext Context { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<int>> roleManager, AppDbContext context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            Context = context;

            IdentitySeedData.Seed(userManager, roleManager).Wait();
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(viewModel.Login,
                    viewModel.Password, viewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser(viewModel.Login) { Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    await SignInManager.PasswordSignInAsync(viewModel.Login,
                        viewModel.Password, true, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Delete user failed.");
                }
            }
            else
            {
                ModelState.AddModelError("", "User was not found.");

            }

            return View("Index", UserManager.Users.ToList());
        }
    }
}
