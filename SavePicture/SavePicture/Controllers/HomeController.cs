using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SavePicture.DAL;
using SavePicture.Models;

namespace SavePicture.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new MyContext())
            {

            }
            return View();
        }


        public JsonResult SaveImage(ImgViewModel model)
        {
            using (var db = new MyContext())
            {
                List<int> imagesId = new List<int>();
                var files = model.ImgFile;
                byte[] imageByte = null;

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        file.SaveAs(Server.MapPath("/ImagesFolder/" + file.FileName));
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        imageByte = reader.ReadBytes(file.ContentLength);

                        Images img = new Images();

                        img.ImgTitle = file.FileName;
                        img.ImgByte = imageByte;
                        img.ImgPath = "/ImagesFolder/" + file.FileName;
                        db.Images.Add(img);
                        db.SaveChanges();

                        imagesId.Add(img.Id);
                    }
                }

                return Json(imagesId, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DisplayingImage(int id)
        {
            using (var db = new MyContext())
            {
                var img = db.Images.SingleOrDefault(x => x.Id == id);
                return File(img.ImgByte, "image/jpg");
            }
        }

        public ActionResult GetAllImages()
        {
            using (var db = new MyContext())
            {
                IEnumerable<Images> img = db.Images.OrderByDescending(x => x.Id).ToList();
                return View(img);
            }
           
        }

        public ActionResult GetImage(int id)
        {
            using (var db = new MyContext())
            {
                var img = db.Images.SingleOrDefault(x => x.Id == id);
                return View(img);
            }
        }
    }
}