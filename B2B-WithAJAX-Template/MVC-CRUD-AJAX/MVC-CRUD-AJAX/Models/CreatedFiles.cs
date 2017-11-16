using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CRUD_AJAX.Models
{
    public class CreatedFiles
    {
        public int Id { get; set; }
        public string FileTitle { get; set; }
        public byte[] FileByte { get; set; }
        public string FilePath { get; set; }
    }
}