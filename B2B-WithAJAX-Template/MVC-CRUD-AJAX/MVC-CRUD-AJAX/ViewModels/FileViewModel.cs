using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD_AJAX.ViewModels
{
    public class FileViewModel
    {
        public List<string> FileName { get; set; }
        public List<int?> FileSize { get; set; }
        public List<string> FileType { get; set; }
        public List<HttpPostedFileWrapper> UploadFile { get; set; }
    }
}