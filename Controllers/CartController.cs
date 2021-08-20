using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Helpers;
using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebShop.Controllers
{
    public class CartController : Controller
    {
        
        private readonly ApplicationDbContext _context;
       
        public CartController( ApplicationDbContext context)
        {
           _context = context;           
        }
        List<Item> cart = new List<Item>();
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cartCount = cart.Count();
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            return View();
        }

        public IActionResult Buy(int id)
        {
            Products productModel = new Products();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                
                cart.Add(new Item { Product = _context.Products.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);

                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = _context.Products.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult OrderProduct()
        {
            //if (cart.Count == 0)
            //{
            //    ViewBag.Msg = "Please add items to cart";
            //}
            //else
            //{
                cart.Clear();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            //}
            return View();
        }

            private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}

