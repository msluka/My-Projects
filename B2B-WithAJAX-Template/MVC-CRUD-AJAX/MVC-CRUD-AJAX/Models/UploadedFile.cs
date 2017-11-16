using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD_AJAX.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int ? FileSize { get; set; }
        public string FileType { get; set; }
        public byte[] FileByte { get; set; }
        public string FilePath { get; set; }
       
    }
}