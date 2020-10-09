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
    public class MenuController : Controller
    {
        private readonly DELMEContext _context;

        public MenuController(DELMEContext context)
        {
            _context = context;
        }

        public Decimal totalPrice;

        // GET: Menu
        public ActionResult Index(int? page, int? count)
        {
            ViewData["CategoryID"] = new SelectList(DAL.CategoryGet(), "ID", "Name");
            List<Menu> menu = DAL.MenuGet();
            ViewBag.Menu = DAL.CategoryGet();
            ViewBag.InCart = GetMenusInCart(Request.Cookies["cartIds"]);
            if (page != null && count != null)
            {
                Page pg = new Page(page, count, menu.Count);
                ViewBag.Pager = pg;
                return View(menu.Skip(pg.Start).Take(pg.CountPerPage));
            }
            else
            {
                return View(menu);
            }
        }

        private List<MenuOrder> GetMenusInCart(string idString)
        {
            List<MenuOrder> retList = new List<MenuOrder>();
            if (!string.IsNullOrEmpty(idString))
            {
                string[] ids = idString.Split(",");
                foreach (string idSt in ids)
                {
                    if (idSt != "")
                    {

                        string[] id_w_quantity = idSt.Split(":");

                        var id = id_w_quantity[0];
                        var quantity = Int32.Parse(id_w_quantity[1]);
                        Menu sp = DAL.MenuGet(id, false);
                        MenuOrder newOrder = new MenuOrder();

                        if (sp != null && newOrder != null)
                        {
                            newOrder.Menu = sp;
                            newOrder.Quantity = quantity;
                            newOrder.ItemPrice = sp.Price;
                            totalPrice += newOrder.Quantity * newOrder.ItemPrice;
                            ViewData[newOrder.ID.ToString()] = newOrder.Quantity * newOrder.ItemPrice;
                            ViewData["TotalPrice"] = totalPrice;
                            retList.Add(newOrder);
                        }
                    }
                }
            }
            return retList;
        }

        public ActionResult AddToCart(int catID)
        {
            List<MenuOrder> inCart = new List<MenuOrder>();
            inCart = GetMenusInCart(Request.Cookies["cartIds"]);
            var alreadyInCart = false;
            //if (inCart.Count == 0)
            //{
            //    alreadyInCart = false;
            //}
            foreach (var cartItem in inCart)
            {
                Menu testMenu = new Menu();
                testMenu = DAL.MenuGet(catID.ToString(), false);
                if (cartItem.Menu.Name.Equals(testMenu.Name))
                {
                    alreadyInCart = true;
                }
            }
            if (!alreadyInCart)
            {
                string cook = Request.Cookies["cartIds"];
                cook = cook == null ? "" : cook;
                cook += catID + ":1" + ",";
                Response.Cookies.Append("cartIds", cook);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        public ActionResult RefreshCart(int? catID, int? quantity)
        {
            string cook = Request.Cookies["cartIds"];
            var indexOfID = cook.IndexOf(catID.ToString());
            var cookCount = cook.Count();
            cook = cook.Remove(indexOfID, 7);


            cook = cook == null ? "" : cook;
            cook += catID + ":" + quantity + ",";
            Response.Cookies.Append("cartIds", cook);
            return Json(new { success = true });
        }

        public ActionResult ProceedToCheckout()
        {
            List<MenuOrder> inCart = new List<MenuOrder>();
            inCart = GetMenusInCart(Request.Cookies["cartIds"]);
            //List<MenuOrder> retList = new List<MenuOrder>();
            var newOrder = new OrderStatus();
            newOrder.OrderTime = DateTime.Now;
            newOrder.UserID = (int)DAL.GetUserForCookie(Request.Cookies["user"]).ID;
            foreach (MenuOrder menuInCart in inCart)
            {
                //var menuOrder = new MenuOrder();
                //menuOrder.MenuID = menuInCart.ID;
                //menuOrder.ItemPrice = menuInCart.Price;
                //menuOrder.Quantity = 2;
                menuInCart.Comment = "BLEH";
                menuInCart.TotalPrice = menuInCart.ItemPrice * menuInCart.Quantity;
                newOrder.MenuOrders.Add(menuInCart);
            }
            DAL.OrderStatusAdd(newOrder);
            return Json(inCart);

        }

        // GET: Menu/Create
        public IActionResult Create()
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.MenuCreate)
            {
                ViewData["CategoryID"] = new SelectList(DAL.CategoryGet(), "ID", "Name");
                return View();
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price, Description, CategoryID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                DAL.MenuAdd(menu);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(DAL.CategoryGet(), "ID", "Name");
            return View(menu);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.MenuEdit)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Menu menu = DAL.MenuGet((int)id);
                if (menu == null)
                {
                    return NotFound();
                }
                ViewData["CategoryID"] = new SelectList(DAL.CategoryGet(), "ID", "Name");
                return View(menu);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Description,CategoryID")] Menu menu)
        {
            if (id != menu.ID)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    DAL.MenuUpdate(menu);
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.ID))
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
            ViewData["CategoryID"] = new SelectList(DAL.CategoryGet(), "ID", "Name");
            return View(menu);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.CategoryEdit)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var menu = DAL.MenuGet((int)id);

                if (menu == null)
                {
                    return NotFound();
                }

                return View(menu);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = DAL.MenuGet((int)id);
            DAL.MenuDelete(menu);

            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.ID == id);
        }
    }
}
