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
    public class CategoryController : Controller
    {

        private readonly DELMEContext _context;

        public CategoryController(DELMEContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index(int? page, int? count)
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.CategoryIndex)
            {
                List<Category> category = DAL.CategoryGet();
                ViewBag.Category = DAL.CategoryGet();

                #region Pagination  
                if (page != null && count != null)
                {
                    Page pg = new Page(page, count, category.Count);
                    ViewBag.Pager = pg;
                    return View(category.Skip(pg.Start).Take(pg.CountPerPage));
                }

                #endregion
                else
                {
                    return View(category);

                }
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.CategoryDetail)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Category category = DAL.CategoryGet((int)id);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.CategoryDelete)
            {
                return View();
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            User roleUser = DAL.GetUserForCookie(Request.Cookies["user"]);
            if (roleUser == null)
            {
                TempData["Error"] = "Please Login First.";
                return RedirectToAction("Index", "Home");
            }
            else if (roleUser.Role.CategoryAdd)
            {
                if (ModelState.IsValid)
                {
                    DAL.CategoryAdd(category);
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

                Category category = DAL.CategoryGet((int)id);

                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DAL.CategoryUpdate(category);
                    //_context.Update(category);
                    //await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
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
            return View(category);
        }

        // GET: Category/Delete/5
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

                Category category = DAL.CategoryGet((int)id);

                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            else
            {
                TempData["ErrorMessage"] = "You do not have permission.";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = DAL.CategoryGet((int)id);

            DAL.CategoryDelete(category);

            return RedirectToAction(nameof(Index));

        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Cart()
        {

            return View();
        }

    }
}

