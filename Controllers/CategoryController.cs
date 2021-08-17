using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categorys.ToList());
        }
        //public ActionResult Details(int id)
        //{
        //    var PList = new List<Products>();
        //    var type = _context.Categorys.Where(s => s.CategoryId == id).FirstOrDefault();
        //    List<Products> products = _context.Products.ToList();
        //    List<Category> categorys = _context.Categorys.ToList();
        //    PList = (from p in products
        //             join c in categorys
        //             on p.Id equals c.CategoryId
        //             where c.CategoryId == id
        //             select p).ToList();
        //    return View(PList);
        //}
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category type)
        {
            if (String.IsNullOrEmpty(type.CategoryName))
            {
                return View();
            }
            else
            {
                _context.Categorys.Add(type);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Edit(int id)
        {
            var type = _context.Categorys.Where(s => s.CategoryId == id).FirstOrDefault();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category type)
        {
            var data = _context.Categorys.Where(s => s.CategoryId == type.CategoryId).FirstOrDefault();

            if (String.IsNullOrEmpty(data.CategoryName) || data.CategoryId == 0)
            {
                return View();
            }
            else
            {
                data.CategoryName = type.CategoryName;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int id)
        {
            var data = _context.Categorys.Where(x => x.CategoryId == id).FirstOrDefault();
            _context.Categorys.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
