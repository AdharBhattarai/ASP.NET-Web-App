using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResturantWebApp.Models;

namespace ResturantWebApp.Controllers
{
    public class MenuOrderController : Controller
    {
        private readonly DELMEContext _context;

        public MenuOrderController(DELMEContext context)
        {
            _context = context;
        }

        // GET: MenuOrder

        public async Task<IActionResult> Index(Menu menu, int userId)
        {
            List<MenuOrder> mOrder = DAL.MenuOrderGet();
            //Models.User cUser = DAL.GetUserForCookie(Request.Cookies["user"]);

            if (ModelState.IsValid)
            {
                MenuOrder menuOrder = new MenuOrder();
                menuOrder.MenuID = menu.ID;
                menuOrder.ItemPrice = menu.Price;
                menuOrder.Quantity = 1;
                menuOrder.TotalPrice = menuOrder.Quantity * menuOrder.ItemPrice;
                menuOrder.Comment = "Hello";
                menuOrder.OrderStatusID = 1;                

                DAL.MenuOrderAdd(menuOrder);
                mOrder.Add(menuOrder);
            }

            return View(mOrder);

        }

        // GET: MenuOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuOrder = await _context.MenuOrder
                .Include(m => m.Menu)
                .Include(m => m.OrderStatus)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menuOrder == null)
            {
                return NotFound();
            }

            return View(menuOrder);
        }

        // GET: MenuOrder/Create
        public IActionResult Create()
        {
            ViewData["MenuID"] = new SelectList(_context.Menu, "ID", "Description");
            ViewData["OrderStatusID"] = new SelectList(_context.OrderStatus, "ID", "ID");
            return View();
        }

        // POST: MenuOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatusID,ID,Quantity,ItemPrice,TotalPrice,Comment,MenuID")] MenuOrder menuOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuID"] = new SelectList(_context.Menu, "ID", "Description", menuOrder.MenuID);
            ViewData["OrderStatusID"] = new SelectList(_context.OrderStatus, "ID", "ID", menuOrder.OrderStatusID);
            return View(menuOrder);
        }

        // GET: MenuOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuOrder = await _context.MenuOrder.FindAsync(id);
            if (menuOrder == null)
            {
                return NotFound();
            }
            ViewData["MenuID"] = new SelectList(_context.Menu, "ID", "Description", menuOrder.MenuID);
            ViewData["OrderStatusID"] = new SelectList(_context.OrderStatus, "ID", "ID", menuOrder.OrderStatusID);
            return View(menuOrder);
        }

        // POST: MenuOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderStatusID,ID,Quantity,ItemPrice,TotalPrice,Comment,MenuID")] MenuOrder menuOrder)
        {
            if (id != menuOrder.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuOrderExists(menuOrder.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuID"] = new SelectList(_context.Menu, "ID", "Description", menuOrder.MenuID);
            ViewData["OrderStatusID"] = new SelectList(_context.OrderStatus, "ID", "ID", menuOrder.OrderStatusID);
            return View(menuOrder);
        }

        // GET: MenuOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuOrder = await _context.MenuOrder
                .Include(m => m.Menu)
                .Include(m => m.OrderStatus)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menuOrder == null)
            {
                return NotFound();
            }

            return View(menuOrder);
        }

        // POST: MenuOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuOrder = await _context.MenuOrder.FindAsync(id);
            _context.MenuOrder.Remove(menuOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuOrderExists(int id)
        {
            return _context.MenuOrder.Any(e => e.ID == id);
        }
    }
}
