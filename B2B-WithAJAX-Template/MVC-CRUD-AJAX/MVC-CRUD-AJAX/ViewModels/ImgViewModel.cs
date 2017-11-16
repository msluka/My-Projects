using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD_AJAX.ViewModels
{
    public class ImgViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public int ProductPrice { get; set; }
        public string ImageId { get; set; }
        public List<HttpPostedFileWrapper> ImgFile { get; set; }
    }
}