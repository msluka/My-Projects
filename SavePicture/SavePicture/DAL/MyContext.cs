using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SavePicture.Models;

namespace SavePicture.DAL
{
    public class MyContext: DbContext
    {
        public MyContext() : base("MyCars") { }
        public virtual DbSet<Images> Images { get; set; }

    }
}