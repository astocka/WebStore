using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderStatusController : Controller
    {
        private readonly AppDbContext _context;

        public OrderStatusController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var statuses = _context.OrderStatuses.ToList();
            return View(statuses);
        }

        // GET: OrderStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        //[Route("/create/")]
        // POST: OrderStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                _context.OrderStatuses.Add(orderStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "OrderStatus");
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View(orderStatus);
        }

        ////// POST: OrderStatus/Edit/5
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(orderStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "OrderStatus");
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var orderStatus = await _context.OrderStatuses.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "OrderStatus");
        }
    }
}
