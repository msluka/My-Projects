using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SavePicture.Models
{
    public class ImgViewModel
    {
        public string CarName { get; set; }
        public List<HttpPostedFileWrapper> ImgFile { get; set; }
    }
}