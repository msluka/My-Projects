using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD_AJAX.DAL;
using MVC_CRUD_AJAX.Models;

namespace MVC_CRUD_AJAX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyContext db = new MyContext();
            var productList = db.Products.ToList();
            return View(productList);
        }

        [HttpPost]
        public JsonResult InsertProduct(Product product)
        {
            using (MyContext db = new MyContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            return Json(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            using (MyContext db = new MyContext())
            {
                Product updatedProduct = (from p in db.Products
                    where p.Id == product.Id
                    select p).FirstOrDefault();
                updatedProduct.Name = product.Name;
                updatedProduct.Unit = product.Unit;
                updatedProduct.Price = product.Price;
                db.SaveChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            using (MyContext db = new MyContext())
            {
                Product product = (from p in db.Products
                    where p.Id == productId
                    select p).FirstOrDefault();
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return new EmptyResult();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}