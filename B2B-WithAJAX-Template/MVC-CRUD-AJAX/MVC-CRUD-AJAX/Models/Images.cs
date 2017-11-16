using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD_AJAX.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string ImgTitle { get; set; }
        public byte[] ImgByte { get; set; }
        public string ImgPath { get; set; }

    }
}