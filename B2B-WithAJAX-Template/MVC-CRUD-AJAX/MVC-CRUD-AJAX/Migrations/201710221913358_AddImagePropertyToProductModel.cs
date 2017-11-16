namespace MVC_CRUD_AJAX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePropertyToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Image_Id", c => c.Int());
            CreateIndex("dbo.Products", "Image_Id");
            AddForeignKey("dbo.Products", "Image_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Image_Id", "dbo.Images");
            DropIndex("dbo.Products", new[] { "Image_Id" });
            DropColumn("dbo.Products", "Image_Id");
        }
    }
}
