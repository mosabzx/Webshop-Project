using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = webHost;
        }

        public async Task<IActionResult> Index()
        {
            dynamic dy = new ExpandoObject();
            dy.PList = getProducts();
            dy.CList = getCategorys();
            return View(dy);
            //return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> CategoryList(int id)
        {
            List<Products> PList = new List<Products>();
            PList = getCategoryProducts(id);
            return View(PList);
        }

        // GET: Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Image/Create
        public IActionResult Create()
        {
            ViewBag.CoursesNames = new SelectList(_context.Categorys, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, ImageName, ImageFile, Quantity, Color, CategoryId")] Products item)
        {
            if (ModelState.IsValid)
            {
                ////save image to wwwroot/Image
                //string wwwRootPath = _hostEnvironment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                //string extension = Path.GetExtension(item.ImageFile.FileName);
                //item.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                //using (var fileStream = new FileStream(path, FileMode.Create))
                //{
                //    await item.ImageFile.CopyToAsync(fileStream);
                //}

                string image = Path.Combine(_hostEnvironment.WebRootPath, "Image");
                string fileName = string.Empty;
                string defaultImage = "default.jpg";
                if (item.ImageFile != null)
                {
                    //string image = Path.Combine(_hostEnvironment.WebRootPath, "Image");
                    fileName = $"{Guid.NewGuid().ToString()}_{item.ImageFile.FileName}";
                    string fullPath = Path.Combine(image, fileName);
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await item.ImageFile.CopyToAsync(fileStream);
                        item.ImageName = fileName;
                    }

                }
                else
                {
                    item.ImageName = defaultImage;

                }

                //Insert Record
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Products.FindAsync(id);
            ViewBag.TypeId = new SelectList(_context.Categorys, "CategoryId", "CategoryName", item.CategoryId);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price, ImageName, ImageFile, Quantity, Color, CategoryId")] Products item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //Todo: Add upload update image.
                    string newfileName;
                    string oldFileName = string.Empty;
                    
                    string image = Path.Combine(_hostEnvironment.WebRootPath, "Image");
                    string defaultImage = "default.jpg";
                    string defaultPath = Path.Combine(image, defaultImage);
                    //Fetching the old image name.
                    oldFileName = item.ImageName;

                    
                    


                    if (item.ImageFile != null)
                    {
                        newfileName = $"{Guid.NewGuid().ToString()}_{item.ImageFile.FileName}";
                        string fullPath = Path.Combine(image, newfileName);
                        

                        //Save the new file.
                        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.ImageFile.CopyToAsync(fileStream);
                            
                        }
                        item.ImageName = newfileName;
                        var fullOLdPath = Path.Combine(image, oldFileName);


                        if (fullOLdPath != defaultPath)
                        {

                            //Delete the old image.
                            System.IO.File.Delete(fullOLdPath);
                        }

                    }
                    

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!ProductsExists(item.Id))
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

            //if (!ModelState.IsValid)
            //{
            //    try
            //    { 
            //    //    string wwwRootPath = _hostEnvironment.WebRootPath;
            //    //    string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
            //    //    string extension = Path.GetExtension(item.ImageFile.FileName);
            //    //    item.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //    //    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            //    //    using (var fileStream = new FileStream(path, FileMode.Create))
            //    //{
            //    //    await item.ImageFile.CopyToAsync(fileStream);
            //    //}

            //        _context.Update(item);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductsExists(item.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(item);
        }

        // GET: Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModel = await _context.Products.FindAsync(id);
            string defaultImage = "default.jpg";
            if (imageModel.ImageName != null && imageModel.ImageName != defaultImage)
            {
                //delete image from wwwroot/image
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", imageModel.ImageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            //delete the record
            _context.Products.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public List<Products> getProducts()
        {
            List<Products> ProductList = _context.Products.Take(12).ToList();
            return ProductList;
        }
        public List<Category> getCategorys()
        {
            List<Category> CategoryList = _context.Categorys.ToList();
            return CategoryList;
        }
        public List<Products> getCategoryProducts(int categoryId)
        {
            List<Products> CategoryProductsList = _context.Products.Where(s => s.CategoryId == categoryId).ToList();
            return CategoryProductsList;
        }
    }
}
