using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        protected UserManager<AppUser> UserManager { get; }
        protected SignInManager<AppUser> SignInManager { get; }
        protected RoleManager<IdentityRole<int>> RoleManager { get; }

        public RoleController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<int>> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        // GET: Role/Index
        public IActionResult Index()
        {
            List<IdentityRole<int>> roles = RoleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            ViewBag.Users = UserManager.Users.ToList();
            return View(roles);
        }

        // GET: Role/Create
        public IActionResult AddRole()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole([Bind("Id,Name")] IdentityRole<int> role)
        {
            if (ModelState.IsValid)
            {
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddToUserRole(string userName)
        {
            AppUser user = await UserManager.FindByNameAsync(userName);
            await UserManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                IdentityRole<int> role = RoleManager.Roles.FirstOrDefault(r => r.Id == id);
                return View(role);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var ir = RoleManager.Roles.FirstOrDefault(r => r.Id == id);
            var result = await RoleManager.DeleteAsync(ir);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.Errors.First().ToString());
                return View("Delete");
            }
            return RedirectToAction("Index", "Role");
        }
    }
}
