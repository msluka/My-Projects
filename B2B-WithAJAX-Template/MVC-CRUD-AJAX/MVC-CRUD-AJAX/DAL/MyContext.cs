using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC_CRUD_AJAX.Models;

namespace MVC_CRUD_AJAX.DAL
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MVCwithAJAX") { }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<CreatedFiles> Files { get; set; }
        public virtual DbSet<UploadedFile> uFiles { get; set; }

    }
}