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
    public class OrderStatusController : Controller
    {
        private readonly DELMEContext _context;

        public OrderStatusController(DELMEContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {

            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";


                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.CategoryEdit)
            {
                List<OrderStatus> orderStatuses = DAL.OrderStatusGet();
                return View(orderStatuses);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }

        }

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // GET: OrderStatus/Create
        //public IActionResult Create(Menu menu, int id)
        //{
            
        //    OrderStatus orderStatus = new OrderStatus();
            

        //    List<MenuOrder> menuOrderList = new List<MenuOrder>();

        //    MenuOrder menuOrder = new MenuOrder();
        //    menuOrder.MenuID = menu.ID;
        //    menuOrder.ItemPrice = menu.Price;
        //    menuOrder.Quantity = 1;
        //    menuOrder.TotalPrice = menuOrder.Quantity * menuOrder.ItemPrice;
        //    menuOrder.Comment = "Hello";
        //    menuOrder.OrderStatusID = orderStatus.ID;
        //    menuOrderList.Add(menuOrder);

        //    orderStatus.MenuOrders.Add(menuOrder);

        //    DAL.MenuOrderAdd(menuOrder);


        //    DAL.OrderStatusAdd(orderStatus);


        //    return View(menuOrder);


        //}



        // POST: OrderStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,OrderTime")] OrderStatus orderStatus)
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (ModelState.IsValid)
            {
                if (roleUser == null)
                {

                    TempData["ErrorMessage"] = "Login.";


                    return RedirectToAction("Index", "Home");
                }
                else if (roleUser.Role.CategoryEdit)
                {
                    DAL.OrderStatusAdd(orderStatus);

                }
                else
                {
                    TempData["ErrorMessage"] = "You do not have permission.";
                    return RedirectToAction("Index", "Home");

                }
            }
            ViewData["UserID"] = new SelectList(DAL.UserGet(), "ID", "Email");
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "ID", "Email", orderStatus.UserID);
            return View(orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,ID,OrderTime")] OrderStatus orderStatus)
        {
            if (id != orderStatus.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderStatus.ID))
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
            ViewData["UserID"] = new SelectList(_context.User, "ID", "Email", orderStatus.UserID);
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderStatus = await _context.OrderStatus.FindAsync(id);
            _context.OrderStatus.Remove(orderStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatus.Any(e => e.ID == id);
        }
    }
}
