namespace MVC_CRUD_AJAX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImgsId", c => c.Int());
            AlterColumn("dbo.Products", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ImgsId");
        }
    }
}
